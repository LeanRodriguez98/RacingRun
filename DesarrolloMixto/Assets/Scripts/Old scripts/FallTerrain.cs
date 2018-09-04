using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTerrain : MonoBehaviour {
    public GameObject startTerrain;
    public float fallSpeed;
    private Vector3 position;
	// Use this for initialization
	void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > 0)
        {
            position.y -= Time.deltaTime * fallSpeed;
            transform.position = position;
            if (transform.position.y <= 0)
            {
                position.y = 0;
                transform.position = position;
            }
        }
	}
}
