using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnTime : MonoBehaviour {
    public float time;
    public GameObject nextGameObject;
	void Start () {
        Invoke("TurnOff",time);
	}

    private void TurnOff()
    {
        gameObject.SetActive(false);
        if (nextGameObject != null)
            nextGameObject.SetActive(true);        
    }
}
