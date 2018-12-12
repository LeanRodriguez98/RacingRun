using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowMovement : MonoBehaviour {

    private Camera mainCamera;
    private Vector3 scale;
    private bool scalingUp = true;

    [Header("MovementSettings")]
    [Space(10)]
    public float rotationSpeed;
    public float scalingSpeed;
    public float MaxSize;
    public float MinSize;

    void Start () {
        scale = new Vector3((MaxSize + MinSize) / 2, (MaxSize + MinSize) / 2, (MaxSize + MinSize) / 2);
        mainCamera = Camera.main;
    }
	
	void Update () {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        float s = scalingSpeed * Time.deltaTime;

        if (scalingUp)
        {
            transform.localScale += new Vector3(s, s, s);
            if (transform.localScale.x > MaxSize)
                scalingUp = false;
        }
        else
        {
            transform.localScale -= new Vector3(s, s, s);
            if (transform.localScale.x < MinSize)
                scalingUp = true;
        }

        transform.LookAt(mainCamera.gameObject.transform);
    }
}
