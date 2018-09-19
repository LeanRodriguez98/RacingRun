using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTerrainPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instance.instancePosition.x = transform.position.x;
        GameManager.instance.instancePosition.z = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
