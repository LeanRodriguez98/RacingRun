using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    #region Singleton
    public static PoolManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    #endregion

    Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Pool po = child.GetComponent<Pool>();
            pools.Add(po.name, po);
        }
    }

    public Pool GetPool(string name) {

        if (pools.ContainsKey(name))
        {
            return pools[name];
        }
        else
        {
            Debug.LogWarning("Name is not on the PoolManager's Dictionary");
            return null;
        }
    }
}
