using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour {

    private UI_Manager uI_ManagerInstance;
    public Text pointsText;
    public Text nutsText;


    private void OnEnable()
    {
        uI_ManagerInstance = UI_Manager.instance;
        pointsText.text = uI_ManagerInstance.playerScore.ToString() + "0";
        nutsText.text = uI_ManagerInstance.playerConis.ToString();
    }
}
