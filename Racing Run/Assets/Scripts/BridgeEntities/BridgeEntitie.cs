using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEntitie : MonoBehaviour {
      
    public bool rotation;
    public float rotationSpeed;
    private Rigidbody rb;
    public int FallMultiplier = 2000;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {

        if (rotation)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

    }

    private void OnEnable()
    {

        if (rb != null)
        {
            rb.AddForce(-Vector3.up * FallMultiplier);
            Debug.Log(gameObject.name);
        }
        else
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(-Vector3.up * FallMultiplier);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);
        }
    }
}
