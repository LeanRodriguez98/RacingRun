using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    

    public static LevelManager instance;

    [HideInInspector] public Vector3 bridgesInstanciePosition = Vector3.zero;
    [HideInInspector] public Vector3 bridgesInstancieRotation = Vector3.zero;

    private ObjectPooler objectPoolerInstance;
    public Bridge[] bridgesInPool;

    [System.Serializable]
    public struct EntitiePatern
    {
        public int metersToSpawn;
        public PoolSpawner.EntityToSpawn[] entity;
    };
    public EntitiePatern[] spawnEntitiePatern;
    private int spawnEntitiePaternIndex = 0;
    private Car carInstance;
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

    }

    public PoolSpawner.EntityToSpawn[] getCurrenSpawnEntity()
    {
        return spawnEntitiePatern[spawnEntitiePaternIndex].entity;
    }

    public void SpawnBridge()
    {
        objectPoolerInstance.SpawnForPool(bridgesInPool[Random.Range(0, bridgesInPool.Length)].gameObject.name/* "BridgeFordward"*/, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
    }

}
