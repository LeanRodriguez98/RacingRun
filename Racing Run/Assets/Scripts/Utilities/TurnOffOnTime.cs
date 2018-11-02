using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnTime : MonoBehaviour {
    public float time;
    public GameObject nextGameObject;
    public bool nextUIShowOnlyOnTutorial;
    public SO_DoTutorial soDoTutorial;
	void Start () {
        Invoke("TurnOff",time);
	}

    private void TurnOff()
    {
        gameObject.SetActive(false);

        if (nextUIShowOnlyOnTutorial)
        {
            if (soDoTutorial.doTutorial)
            {
                if (nextGameObject != null)
                    nextGameObject.SetActive(true);
            }
        }
        else
        {
            if (nextGameObject != null)
                nextGameObject.SetActive(true);
        }
               
    }
}
