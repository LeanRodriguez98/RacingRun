using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CanvasButtons : MonoBehaviour {

    public Animator shopAnimator;
    public GameObject shopPanel;
    public void OpenShop()
    {
        shopPanel.SetActive(true);
        shopAnimator.SetTrigger("StoreOpen");
    }
    public void CloseShop()
    {
        shopAnimator.SetTrigger("StoreClose");
        Invoke("TurnOffPanel", 3.0f);

    }
    public void TurnOffPanel()
    {
        shopPanel.SetActive(false);
    }
}
