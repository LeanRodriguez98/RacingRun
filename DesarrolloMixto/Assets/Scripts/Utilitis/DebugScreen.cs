using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScreen : MonoBehaviour {

    public GameObject[] screenElements;
    private bool screenEnabled = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChanheStateScreen()
    {
        if (screenEnabled)
        {
            for (int i = 0; i < screenElements.Length; i++)
            {
                screenElements[i].SetActive(false);
            }
            screenEnabled = false;
        }
        else
        {
            for (int i = 0; i < screenElements.Length; i++)
            {
                screenElements[i].SetActive(true);
            }
            screenEnabled = true;
        }
    }
}
