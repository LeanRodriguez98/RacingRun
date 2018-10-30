using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEntitie : MonoBehaviour {


    private Ray heightRay;
    private RaycastHit heightRayHit;
    public float floorDistance;
    public float fallSpeed;
    public bool rotation;
    public float rotationSpeed;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        heightRay = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(heightRay, out heightRayHit))
        {
            if (heightRayHit.distance > floorDistance)
            {
                transform.position -= Vector3.up * fallSpeed * Time.deltaTime;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }


        if (rotation)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

    }
}
