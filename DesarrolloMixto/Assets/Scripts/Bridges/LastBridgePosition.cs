using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBridgePosition : MonoBehaviour {
    private GameManager gamemanagerInstance;

    private void Start()
    {
        gamemanagerInstance = GameManager.instance;
        gamemanagerInstance.instancePosition.x = transform.position.x;
        gamemanagerInstance.instancePosition.z = transform.position.z;

    }
  
	
	// Update is called once per frame
	void Update () {
        
	}
}
