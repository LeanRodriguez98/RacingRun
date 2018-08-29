using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTerrainPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instancie.instancePosition.x = transform.position.x;
        GameManager.instancie.instancePosition.z = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
