using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    private Vector3 rotation;
    public float rotateSpeed;
	// Use this for initialization
	void Start () {
        rotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        rotation.y += rotateSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
