using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour {

    private Rigidbody rb;
    private BoxCollider[] bc;
    private Car carInstance;
    public GameObject floorCollider;
    public float expultionForce = 10000;
    public float minRandomY = 0.2F;
    public float maxRandomY = 1.0F;
    public float resetTime = 5;
    void Start () {
        rb = GetComponent<Rigidbody>();
        bc = GetComponentsInChildren<BoxCollider>();
        carInstance = Car.instance;
        
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
            for (int i = 0; i < bc.Length; i++)            
                bc[i].enabled = false;
            
            floorCollider.SetActive(false);
            Vector3 direction = Vector3.zero;
            direction = transform.position - carInstance.transform.position;

            direction.y += Random.Range(minRandomY, maxRandomY);
            direction.Normalize();

            rb.AddForce(direction * expultionForce);
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
            for (int i = 0; i < bc.Length; i++)            
                bc[i].enabled = true;            
        floorCollider.SetActive(true);
    }
}
