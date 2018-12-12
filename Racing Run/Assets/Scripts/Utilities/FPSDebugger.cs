using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDebugger : MonoBehaviour
{
    private int frameCount = 0;
    private float dt = 0.0F;
    private float fps = 0.0F;

    [Header("UpdateRateSeconds")]
    [Space(10)]
    public float updateRateSeconds = 4.0F;
    [Header("TextFormat")]
    [Space(10)]
    public string formatedString = "{value} FPS";
    [Header("Text")]
    [Space(10)]
    public Text txtFps;


    void Update()
    {
        frameCount++;
        dt += Time.unscaledDeltaTime;
        if (dt > 1.0 / updateRateSeconds)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0F / updateRateSeconds;
        }
        txtFps.text = formatedString.Replace("{value}", System.Math.Round(fps, 1).ToString("0.0"));
    }
}
