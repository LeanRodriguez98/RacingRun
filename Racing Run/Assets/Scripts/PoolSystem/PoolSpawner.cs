using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour {
    private ObjectPooler objectPoolerInstance;
    public GameObject objectToSpawn;
    void Start () {
        objectPoolerInstance = ObjectPooler.instance;
        Invoke("SpawnObject", 1f);
    }

    public void SpawnObject()
    {
        objectPoolerInstance.SpawnForPool(objectToSpawn.name, transform.position, Quaternion.identity);

    }

   
}

