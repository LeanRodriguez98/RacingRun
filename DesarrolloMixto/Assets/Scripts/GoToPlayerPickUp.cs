using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPlayerPickUp : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 direction;
    private Player playerInstance;
    public int movementForce;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInstance = Player.instance;
    }
    private void OnMouseDown()
    {

        direction = playerInstance.transform.position - transform.position;
        direction.Normalize();
        rb.AddForce(direction* movementForce);
    }
}
