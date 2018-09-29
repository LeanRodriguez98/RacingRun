using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWaterTrigger : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            other.gameObject.SetActive(false);
        }
    }
}
