using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpdateLifeBar : MonoBehaviour {

    private Car carInstance;
    private RectTransform rectTransform;
    private Vector2 rectSize;
    private float originalHeight;
    private int auxCarLife;
    // Use this for initialization
    void Start () {
        carInstance = Car.instance;
        auxCarLife = carInstance.life;
        rectTransform = GetComponent<RectTransform>();
        rectSize = rectTransform.sizeDelta;
        originalHeight = rectSize.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (auxCarLife != carInstance.life)
        {
            rectSize.y = (carInstance.life * originalHeight) / 100;
            rectTransform.sizeDelta = rectSize;
            auxCarLife = carInstance.life;
        }
    }
}
