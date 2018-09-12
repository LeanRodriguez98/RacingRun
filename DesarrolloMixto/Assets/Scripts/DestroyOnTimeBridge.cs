using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimeBridge : MonoBehaviour {

    public Rigidbody rb;
  

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarCenter")
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            Destroy(transform.parent.parent.gameObject, 3);
        }
    }    
}
