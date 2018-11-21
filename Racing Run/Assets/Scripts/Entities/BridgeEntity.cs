using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEntity : MonoBehaviour {
      
    public bool rotation;
    public float rotationSpeed;
    private Rigidbody rb;
    public int FallMultiplier = 2000;
    private ObjectPooler objectPoolerInstance;
    public GameObject particles;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        objectPoolerInstance = ObjectPooler.instance;

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
        }
        else
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(-Vector3.up * FallMultiplier);

        }

    }

    private void OnDisable()
    {
        if(particles != null && objectPoolerInstance != null)
            objectPoolerInstance.SpawnForPool(particles.gameObject.name, transform.position, transform.rotation);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);
        }
    }
}
