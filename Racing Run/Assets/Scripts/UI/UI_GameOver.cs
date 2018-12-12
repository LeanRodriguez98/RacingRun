using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour {

    private UI_Manager uI_ManagerInstance;
    [Header("Texts")]
    [Space(10)]
    [Header("       Texts - Ponts")]
    [Space(5)]
    public Text pointsText;
    [Header("       Texts - Nuts")]
    [Space(5)]
    public Text nutsText;


    private void OnEnable()
    {
        uI_ManagerInstance = UI_Manager.instance;
        pointsText.text = uI_ManagerInstance.playerScore.ToString() + " Mts";
        nutsText.text = uI_ManagerInstance.playerConis.ToString();
    }
}
