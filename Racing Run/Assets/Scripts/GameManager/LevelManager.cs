using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    [HideInInspector] public Vector3 bridgesInstanciePosition = Vector3.zero;
    [HideInInspector] public Vector3 bridgesInstancieRotation = Vector3.zero;

    private ObjectPooler objectPoolerInstance;
    public Bridge[] bridgesInPool;


    private void Awake()
    {
        instance = this; 
    }

    void Start ()
    {
        objectPoolerInstance = ObjectPooler.instance;
    }
	
	void Update ()
    {
		
	}

    public void SpawnBridge()
    {
        objectPoolerInstance.SpawnForPool(bridgesInPool[Random.Range(0, bridgesInPool.Length)].gameObject.name, bridgesInstanciePosition, Quaternion.Euler(bridgesInstancieRotation));
    }
}
