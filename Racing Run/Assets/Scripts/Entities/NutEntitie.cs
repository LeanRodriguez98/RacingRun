using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutEntitie : MonoBehaviour {
    private Rigidbody rb;
    private BoxCollider bc;
    public GameObject floorCollider;
    public float expultionForce = 10000;
    public float resetTime = 5;
    private Camera cam;
        
    void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        RestartObject();

    }

    private void OnDisable()
    {

    }

    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            rb.constraints = RigidbodyConstraints.None;
            bc.enabled = false;
            floorCollider.SetActive(false);

          //  Vector3 direction = CoinsPanel.transform.position - transform.position;
         //   direction.Normalize();
        //    rb.AddForce(direction * expultionForce);
            Invoke("RestartObject", resetTime);
        }

        if (other.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);

        }
    }

    private void RestartObject()
    {
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        transform.eulerAngles = Vector3.zero;
        if (bc != null)
            bc.enabled = true;
        floorCollider.SetActive(true);
    }
}
