using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnArrow : MonoBehaviour {

    public GameObject bezierTurn;
    private Player playerInstance;
    private Vector3 turnRotation;
    private bool Instaciated = true;
    private void Start()
    {
        playerInstance = Player.instance;
        turnRotation = playerInstance.transform.eulerAngles;
        turnRotation.y -= 180;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (Instaciated)
            {
                Instantiate(bezierTurn, playerInstance.transform.position + (playerInstance.transform.forward * 4), Quaternion.Euler(0, playerInstance.transform.eulerAngles.y - 180, 0));
                Instaciated = false;
            }
            Destroy(gameObject);

        }
    }
}
