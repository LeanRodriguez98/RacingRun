using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutEntity : MonoBehaviour {
    private ObjectPooler objectPoolerInstance;
    private Rigidbody rb;
    private BoxCollider bc;
    [Header("FloorCollider")]
    [Space(10)]
    public GameObject floorCollider;
    [Header("ResetTime")]
    [Space(10)]
    public float resetTime = 5;
    void Start () {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        objectPoolerInstance = ObjectPooler.instance;
    }

    private void OnEnable()
    {
        RestartObject();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            rb.constraints = RigidbodyConstraints.None;
            bc.enabled = false;
            floorCollider.SetActive(false);



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
