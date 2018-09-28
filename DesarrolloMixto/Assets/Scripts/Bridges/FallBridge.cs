using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBridge : MonoBehaviour {
    public float fallSpeed;
    private Vector3 position;
    private float time;

    // Use this for initialization
    void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if (transform.position.y > 0)
        {
            position.y -= time * fallSpeed;
            transform.position = position;
            if (transform.position.y <= 0)
            {
                position.y = 0;
                transform.position = position;
            }
        }
	}
}
