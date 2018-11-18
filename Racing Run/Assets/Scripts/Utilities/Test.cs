using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private LevelManager levelManagerInstance;

    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public GameObject b4;
    public GameObject b5;
    // Use this for initialization
    void Start () {
        levelManagerInstance = LevelManager.instance;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            levelManagerInstance.SpawnBridge(b1.gameObject.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            levelManagerInstance.SpawnBridge(b2.gameObject.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            levelManagerInstance.SpawnBridge(b3.gameObject.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            levelManagerInstance.SpawnBridge(b4.gameObject.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            levelManagerInstance.SpawnBridge(b5.gameObject.name);
        }
    }
}
