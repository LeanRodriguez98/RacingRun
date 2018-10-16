using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TutorialPanel : MonoBehaviour {
    
    public RawImage image;
    public float fadeDelay;
    public float angleRotation;
    public float movingSpeed;
    private float auxTime;
    private Color CurrentColor;
    private enum ImageState {FadeOn,Moving,FadeOff};
    private ImageState state = ImageState.FadeOn;
    private enum DirectionMoving {Right,Left};
    private DirectionMoving movingState = DirectionMoving.Right;
    private Vector3 currentRotation;
    private bool go = true;
    void Start()
    {
        CurrentColor.b = 255;
        CurrentColor.g = 255;
        CurrentColor.r = 255;
        CurrentColor.a = 0;
        image.color = CurrentColor;
        currentRotation = Vector3.zero;
    }

    void Update()
    {
        switch (state)
        {
            case ImageState.FadeOn:
                CurrentColor.a += Time.deltaTime / fadeDelay;
                if (CurrentColor.a > 1)
                {
                    CurrentColor.a = 1;
                    state = ImageState.Moving;
                }
                image.color = CurrentColor; 
                break;
            case ImageState.Moving:

               
                    
                break;
            case ImageState.FadeOff:
                CurrentColor.a -= Time.deltaTime / fadeDelay;
                if (CurrentColor.a < 0)
                {
                    CurrentColor.a = 0;
                    gameObject.SetActive(false);
                }
                image.color = CurrentColor;
                break;
        }


    }
}
