using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCollider : MonoBehaviour {
    public bool collision = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terain")
        {
            collision = true;
        }

    }

    public bool Collider()
    {

        if (collision)
        {
            return true;
        }
        return false;

    }
}
