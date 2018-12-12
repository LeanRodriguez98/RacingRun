using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnTime : MonoBehaviour {
    [Header("TutorialData")]
    [Space(10)]
    public SO_DoTutorial soDoTutorial;
    [Header("NextGameObject")]
    [Space(10)]
    public float time;
    public GameObject nextGameObject;
    public bool nextUIShowOnlyOnTutorial;
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
