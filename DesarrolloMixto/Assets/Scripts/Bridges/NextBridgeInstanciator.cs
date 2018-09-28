using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBridgeInstanciator : MonoBehaviour {
    private GameManager gamemanagerInstance;
    private bool instancied = false;

    private void Start()
    {
        gamemanagerInstance = GameManager.instance;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarCenter" && !instancied)
        {
            gamemanagerInstance.Instanciator();
            instancied = true;
        }
    }
}
