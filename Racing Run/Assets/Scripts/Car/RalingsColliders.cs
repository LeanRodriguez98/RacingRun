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
            Debug.Log("Entra" + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RailingCollider")
        {
            isTrigger = false;
            Debug.Log("Sale" + gameObject.name);

        }
    }
}
