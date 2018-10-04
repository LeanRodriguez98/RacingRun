using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnArrow : MonoBehaviour {

    public GameObject bezierTurn;
    private Player playerInstance;
    private Vector3 turnRotation;

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
            Destroy(gameObject);
            Instantiate(bezierTurn, transform.position + (playerInstance.transform.forward * 4), Quaternion.Euler(turnRotation));
        }
    }
}
