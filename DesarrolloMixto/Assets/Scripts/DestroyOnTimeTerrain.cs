using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimeTerrain : MonoBehaviour {



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Destroy(transform.parent.parent.gameObject, 3);
        }
    }

    
}
