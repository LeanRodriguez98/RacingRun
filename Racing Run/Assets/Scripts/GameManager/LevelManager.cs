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
    public EntitiePatern[] spawnEntitiePatern;
    private int spawnEntitiePaternIndex = 0;
    [HideInInspector] public Car carInstance;
    [HideInInspector] public float startToSpawnDelay = 2.0f;
    private int tutorialStep = 0;

    public SO_DoTutorial soDoTutorial;

    public GameObject FirstGameBridge;
    public GameObject FirstTutorialBridge;

    private void Awake()
    {
        instance = this; 
    }

    void Start ()
    {
        objectPoolerInstance = ObjectPooler.instance;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        carInstance = Car.instance;

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
	
	void Update ()
    {
        for (int i = 0; i < spawnEntitiePatern.Length; i++)
        {
            if ((int)carInstance.metersTraveled == spawnEntitiePatern[i].metersToSpawn)
            {
                spawnEntitiePaternIndex = i;
            }
        }

        if (startToSpawnDelay >= 0)
            startToSpawnDelay -= Time.deltaTime;

        if (carInstance.tutorialEnded)
        {
            carInstance.tutorialEnded = false;
            soDoTutorial.doTutorial = false;
            for (int i = 0; i < spawnEntitiePatern.Length; i++)
            {
                spawnEntitiePatern[i].metersToSpawn += ((int)carInstance.metersTraveled - spawnEntitiePatern[1].metersToSpawn);
            }
        }
    }

    public PoolSpawner.EntityToSpawn[] getCurrenSpawnEntity()
    {
        return spawnEntitiePatern[spawnEntitiePaternIndex].entity;
    }

    public void SpawnBridge()
    {
        objectPoolerInstance.SpawnForPool(bridgesInPool[Random.Range(0, bridgesInPool.Length)].gameObject.name, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
    }
    public void SpawnTutorialBridge()
    {
        objectPoolerInstance.SpawnForPool(bridgesOfTutorial[tutorialStep].gameObject.name, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
        tutorialStep++;
    }

}
