using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    

    public static LevelManager instance;

    [HideInInspector] public Vector3 bridgesInstanciePosition = Vector3.zero;
    [HideInInspector] public Vector3 bridgesInstancieRotation = Vector3.zero;

    private ObjectPooler objectPoolerInstance;
    public Bridge[] bridgesOfTutorial;
    public Bridge[] bridgesInPool;
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
    public EntitiePatern[] spawnEntitiePatern;
    public TrainBarrierPattern[] spawnTrainBarrierPattern;
    private int spawnEntitiePatternIndex = 0;
    private int spawnTrainBarrierPatternIndex = 0;
    [HideInInspector] public Car carInstance;
    [HideInInspector] public float startToSpawnDelay = 2.0f;
    private int tutorialStep = 0;
    private GameSaveManager gameSaveManagerInstance;
    public SO_DoTutorial soDoTutorial;

    public GameObject FirstGameBridge;
    public GameObject FirstTutorialBridge;

    private AudioManager audioManagerInstance;
    [Header("AudioClips")]
    [Space(10)]
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
