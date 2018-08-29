using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Vector3 cameraOffset;
    public Vector3 cameraRotation;
    private Vector3 cameraPosition;
    public GameObject Player;
    // Use this for initialization
    void Start()
    {
        cameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            cameraPosition.x = Player.transform.position.x + cameraOffset.x;
            cameraPosition.y = Player.transform.position.y + cameraOffset.y;
            cameraPosition.z = Player.transform.position.z + cameraOffset.z;
            transform.position = cameraPosition;
            transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
        }
        
    }
}
