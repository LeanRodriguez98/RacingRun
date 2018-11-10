using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RalingsColliders : MonoBehaviour {

    [HideInInspector] public bool isTrigger = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RailingCollider")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "RailingCollider")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RailingCollider")
        {
            isTrigger = false;

        }
    }
}
