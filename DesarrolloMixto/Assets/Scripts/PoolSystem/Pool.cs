using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    public GameObject gameObjectPrefab;
    public int cantObjects = 10;

    List<GameObject> objects = new List<GameObject>();
    int objIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < cantObjects; i++)
        {
            GameObject go;
            go = Instantiate(gameObjectPrefab);
            go.SetActive(false);
            PoolObject po = go.AddComponent<PoolObject>();
            po.SetPool(this);
            objects.Add(go);
        }
    }

    public void AddToList(GameObject obj) {
        objIndex--;
        objects[objIndex] = obj;
        obj.SetActive(false);
    }

    public GameObject UseObj() {
        GameObject returnObj = objects[objIndex];
        returnObj.SetActive(true);
        objIndex++;

        return returnObj;
    }
}
