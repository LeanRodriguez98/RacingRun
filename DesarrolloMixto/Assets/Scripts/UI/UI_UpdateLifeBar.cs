using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpdateLifeBar : MonoBehaviour {

    private Player playerInstance;
    private RectTransform rectTransform;
    private Vector2 rectSize;
    private float originalHeight;
    private int auxPlayerLife;
    // Use this for initialization
    void Start () {
        playerInstance = Player.instance;
        auxPlayerLife = playerInstance.life;
        rectTransform = GetComponent<RectTransform>();
        rectSize = rectTransform.sizeDelta;
        originalHeight = rectSize.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (auxPlayerLife != playerInstance.life)
        {
            rectSize.y = (playerInstance.life * originalHeight) / 100;
            rectTransform.sizeDelta = rectSize;
            auxPlayerLife = playerInstance.life;
        }
    }
}
