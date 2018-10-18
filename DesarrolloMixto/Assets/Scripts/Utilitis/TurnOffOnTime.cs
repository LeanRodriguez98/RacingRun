using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnTime : MonoBehaviour {
    public int time;
	// Use this for initialization
	void Start () {
        Invoke("TurnOff",time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
