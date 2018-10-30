using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEntitie : MonoBehaviour {
      
    public bool rotation;
    public float rotationSpeed;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        if (rotation)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);
        }
    }
}
