using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private AudioManager audioManagerInstance;
    private ObjectPooler objectPoolerInstance;
    private GameSaveManager gameSaveManagerInstance;
    private Car carInstance;


    public static LevelManager instance;

    [System.Serializable]
    public struct EntitiePatern
    {
        public int metersToSpawn;
        public PoolSpawner.EntityToSpawn[] entity;
    };
    [System.Serializable]
    public struct TrainBarrierPattern
    {
        public int metersToSpawn;
        public float TrainBarrierProbability;
    }
    [HideInInspector] public float startToSpawnDelay = 2.0f;
    [HideInInspector] public Vector3 bridgesInstanciePosition = Vector3.zero;
    [HideInInspector] public Vector3 bridgesInstancieRotation = Vector3.zero;

    [Header("Tutorial")]
    [Space(10)]
    public SO_DoTutorial soDoTutorial;
    public GameObject FirstTutorialBridge;
    public Bridge[] bridgesOfTutorial;
    private int tutorialStep = 0;
    [Header("Gameplay")]
    [Space(10)]
    public GameObject FirstGameBridge;
    public Bridge[] bridgesInPool;
    public EntitiePatern[] spawnEntitiePatern;
    public TrainBarrierPattern[] spawnTrainBarrierPattern;
    private int spawnEntitiePatternIndex = 0;
    private int spawnTrainBarrierPatternIndex = 0;
    [Header("AudioClips")]
    [Space(10)]
    [Header("       AudioClips - Music")]
    [Space(5)]
    public AudioManager.Clip musicIntro;
    public AudioManager.Clip musicLoop;
    [Header("       AudioClips - 321GOSounds")]
    [Space(5)]
    public AudioManager.Clip numberSound;
    public AudioManager.Clip goSound;
    public float timeBetweenSounds;
    private int startAnimationSteps = 3;

    private void Awake()
    {
        instance = this; 
    }

    void Start ()
    {
        gameSaveManagerInstance = GameSaveManager.instance;
        gameSaveManagerInstance.LoadGame(soDoTutorial);
        objectPoolerInstance = ObjectPooler.instance;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        carInstance = Car.instance;
        audioManagerInstance = AudioManager.instance;
        Play321GoSound(numberSound);
        audioManagerInstance.PlayTriggerMusic(musicIntro.clip, musicIntro.Volume);
        this.Invoke("PlayGameplayLoopMusic", musicLoop, musicIntro.clip.length - 0.35f);
        if (soDoTutorial.doTutorial)
        {
            FirstGameBridge.SetActive(false);
            FirstTutorialBridge.SetActive(true);
        }

        if (!soDoTutorial.doTutorial)
        {
            FirstGameBridge.SetActive(true);
            FirstTutorialBridge.SetActive(false);
        }
    }

    public void PlayGameplayLoopMusic(AudioManager.Clip audioClip)
    {
        audioManagerInstance.PlayMusic(audioClip.clip, audioClip.Volume);
    }


    public void Play321GoSound(AudioManager.Clip audioClip)
    {
        audioManagerInstance.PlayTriggerSound(audioClip.clip, audioClip.Volume);
        startAnimationSteps--;
        if (startAnimationSteps > 0)
            this.Invoke("Play321GoSound", numberSound, timeBetweenSounds);
        else if (startAnimationSteps == 0)
            this.Invoke("Play321GoSound", goSound, timeBetweenSounds);
    }

    void Update ()
    {
        for (int i = 0; i < spawnEntitiePatern.Length; i++)
        {
            if ((int)carInstance.metersTraveled == spawnEntitiePatern[i].metersToSpawn)
            {
                spawnEntitiePatternIndex = i;
            }
        }

        for (int i = 0; i < spawnTrainBarrierPattern.Length; i++)
        {
            if ((int)carInstance.metersTraveled == spawnTrainBarrierPattern[i].metersToSpawn)
            {
                spawnTrainBarrierPatternIndex = i;
            }
        }

        if (startToSpawnDelay >= 0)
            startToSpawnDelay -= Time.deltaTime;

        if (carInstance.tutorialEnded)
        {
            carInstance.tutorialEnded = false;
            soDoTutorial.doTutorial = false;
            gameSaveManagerInstance.SaveGame(soDoTutorial);
            for (int i = 0; i < spawnEntitiePatern.Length; i++)
            {
                spawnEntitiePatern[i].metersToSpawn += ((int)carInstance.metersTraveled - spawnEntitiePatern[1].metersToSpawn);
            }
        }
    }

    public PoolSpawner.EntityToSpawn[] getCurrenSpawnEntity()
    {
        return spawnEntitiePatern[spawnEntitiePatternIndex].entity;
    }

    public float getCurrentTrainBarrierSpawnPrbability()
    {
        return spawnTrainBarrierPattern[spawnTrainBarrierPatternIndex].TrainBarrierProbability;
    }
    public void SpawnBridge()
    {
        objectPoolerInstance.SpawnForPool(bridgesInPool[Random.Range(0, bridgesInPool.Length)].gameObject.name, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
    }
    public void SpawnBridge(string bridgeName)
    {
        objectPoolerInstance.SpawnForPool(bridgeName, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
    }
    public void SpawnTutorialBridge()
    {
        objectPoolerInstance.SpawnForPool(bridgesOfTutorial[tutorialStep].gameObject.name, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
        tutorialStep++;
    }

}
