using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [HideInInspector] public string tag;
    public GameObject prefab;
    public int size;
}



public class ObjectPooler : MonoBehaviour
{

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            pool.tag = pool.prefab.gameObject.name;

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                go.SetActive(false);
                objectPool.Enqueue(go);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnForPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " dosen't exist in " + gameObject.name);
            return null;
        }
        GameObject goToSpawn = poolDictionary[tag].Dequeue();
        goToSpawn.SetActive(true);
        goToSpawn.transform.position = position;
        goToSpawn.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(goToSpawn);

        return goToSpawn;
    }
}

