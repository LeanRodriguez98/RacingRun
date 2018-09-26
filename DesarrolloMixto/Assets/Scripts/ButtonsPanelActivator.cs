using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsPanelActivator : MonoBehaviour {

    private GameObject buttonsPanel;

    private void Start()
    {
        buttonsPanel = GameManager.instance.OptionsPanel;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarCenter")
        {
            buttonsPanel.SetActive(true);
        }
    }
}
