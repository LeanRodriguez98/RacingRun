using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    public static UI_Manager instance;
    public Text lifeText;
    public Text scoreText;
    public Text coinsText;
    [HideInInspector]public int playerScore;
    private int score;
    private int playerLife;
    private int life;
    [HideInInspector] public int playerConis;
    private int coins;
    private Player playerInstance;
    private GameManager gameManagerInstance;

    public GameObject gameOverPanel;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        playerInstance = Player.instance;
        gameManagerInstance = GameManager.instance;
        playerScore = score = playerLife = life = playerConis = coins = 0;
    }

    void Update () {
        if (playerInstance.gameObject.activeSelf)
        {



            if (gameManagerInstance != null)
                playerScore = (int)gameManagerInstance.points;
            if (playerInstance != null)
            {
                playerLife = playerInstance.life;
                playerConis = playerInstance.nuts;
            }

            if (playerScore != score)
            {
                score = playerScore;
                scoreText.text = " " + score.ToString() + "0";
            }

            if (playerLife != life)
            {
                life = playerLife;
                lifeText.text = life.ToString();
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
