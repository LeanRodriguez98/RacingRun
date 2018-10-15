using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public Text lifeText;
    public Text scoreText;
    public Text coinsText;
    private int playerScore;
    private int score;
    private int playerLife;
    private int life;
    private int playerConis;
    private int coins;
    private Player playerInstance;
    private GameManager gameManagerInstance;


    void Start () {
        playerInstance = Player.instance;
        gameManagerInstance = GameManager.instance;
        playerScore = score = playerLife = life = playerConis = coins = 0;
    }

    void Update () {
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
            scoreText.text = score.ToString() + "0";
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
}
