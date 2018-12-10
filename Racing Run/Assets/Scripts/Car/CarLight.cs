using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLight : MonoBehaviour {
    [HideInInspector] public MeshRenderer mesh;
    public int breakProbability;


    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (Random.Range(0,100) < breakProbability)
            {
                mesh.enabled = false;
            }
        }
    }

    public void Repair()
    {
        mesh.enabled = false;
    }
}
