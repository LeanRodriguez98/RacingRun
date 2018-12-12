using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEntity : MonoBehaviour {

    private ObjectPooler objectPoolerInstance;
    private Rigidbody rb;
    private bool touchWater = false;
    [Header("RotationSettings")]
    [Space(10)]
    public bool rotation;
    public float rotationSpeed;
    [Header("FallSettings")]
    [Space(10)]
    public int FallMultiplier = 2000;
  
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
        touchWater = false;
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



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            touchWater = true;

            gameObject.SetActive(false);
        }
    }
}
