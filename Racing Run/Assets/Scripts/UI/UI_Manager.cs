using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    public static UI_Manager instance;
   // public Text lifeText;
    public Text metersText;
    public Text coinsText;
    [HideInInspector]public int playerScore;
    private int meters;
    private int playerLife;
    private int life;
    [HideInInspector] public int playerConis;
    private int coins;
    private Car carInstance;
    private LevelManager levelManagerInstance;

    public GameObject gameOverPanel;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        carInstance = Car.instance;
        levelManagerInstance = LevelManager.instance;
        playerScore = meters = playerLife = life = playerConis = coins = 0;
    }

    void Update () {
        if (carInstance.gameObject.activeSelf)
        {



            if (levelManagerInstance != null)
                playerScore = (int)carInstance.metersTraveled;
            if (carInstance != null)
            {
                playerLife = carInstance.life;
                playerConis = carInstance.nuts;
            }

            if (playerScore != meters)
            {
                meters = playerScore;
                metersText.text = " " + meters.ToString() + " Mts";
            }

            if (playerLife != life)
            {
                life = playerLife;
              //  lifeText.text = life.ToString();
            }

            if (playerConis != coins)
            {
                coins = playerConis;
                coinsText.text = coins.ToString();
            }
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }
}
