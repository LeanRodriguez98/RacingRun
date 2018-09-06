using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimeTerrain : MonoBehaviour {



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Destroy(gameObject, 2);

        }

    }

    
}
