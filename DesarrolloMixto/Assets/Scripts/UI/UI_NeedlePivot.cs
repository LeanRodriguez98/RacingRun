using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NeedlePivot : MonoBehaviour {
    private Player playerInstance;
    private Vector3 needleRotation;
	// Use this for initialization
	void Start () {
        playerInstance = Player.instance;
        needleRotation = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        needleRotation.z = -(playerInstance.Speed * 135) / playerInstance.MaxSpeed;
        transform.eulerAngles = needleRotation;
    }
}
