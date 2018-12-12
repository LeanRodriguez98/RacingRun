using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour {
    private Car carInstance;
    private Rigidbody rb;
    private BoxCollider[] bc;
    [Header("FloorCollider")]
    [Space(10)]
    public GameObject floorCollider;
    [Header("ExpultionSettings")]
    [Space(10)]
    public float expultionForce = 100;
    public float playerNitroExpultionForceMultipler = 3;
    [Header("SpawnSettings")]
    [Space(10)]
    public float minRandomY = 0.2F;
    public float maxRandomY = 1.0F;
    [Header("ResetTime")]
    [Space(10)]
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
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            Vector3 direction = Vector3.zero;
            direction = transform.position - carInstance.transform.position;


        
            direction.y += Random.Range(minRandomY, maxRandomY);
            direction.Normalize();

            if (carInstance.nitro)
                rb.AddForce(direction * (expultionForce * playerNitroExpultionForceMultipler));
            else
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
