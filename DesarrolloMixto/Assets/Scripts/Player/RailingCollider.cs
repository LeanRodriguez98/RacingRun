using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailingCollider : MonoBehaviour {
    [HideInInspector] public bool isTrigger = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RailingColliders")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RailingColliders")
        {
            isTrigger = false;
        }
    }
}
