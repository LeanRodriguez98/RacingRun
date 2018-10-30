using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {
    public Transform endPosition;
    public TriggerCollider endBridgeCollider;
    public TriggerCollider nextBridgeCollider;
    public float exitRotationY;
    public float startHeight;
    public float endHeight;
    public float fallSpeed;

    public bool haveRailings;
    public GameObject[] railings;
    public bool haveEntities;
    public GameObject[] entities;

    private Vector3 position;
    private Vector3 rotation;
    private LevelManager levelManagerInstance;
    private bool nextInstance;

    private void Start()
    {
        levelManagerInstance = LevelManager.instance;
    }

    void OnEnable () {
        //position =transform.position;
        //Debug.Log(levelManagerInstance.bridgesInstanciePosition);
        position.y = startHeight;
        //transform.position = position;
        //rotation = levelManagerInstance.bridgesInstancieRotation;
        //transform.rotation = Quaternion.Euler(rotation);
        endBridgeCollider.isTrigger = false;
        nextBridgeCollider.isTrigger = false;
        nextInstance = false;

        for (int i = 0; i < railings.Length; i++)
        {
            if (haveRailings)
            {
                railings[i].SetActive(true);
            }
            else
            {
                railings[i].SetActive(false);
            }
        }

        for (int i = 0; i < entities.Length; i++)
        {
            if (haveEntities)
            {
                entities[i].SetActive(true);
            }
            else
            {
                entities[i].SetActive(false);
            }
        }
    }

    

    void Update () {
        if (!endBridgeCollider.isTrigger)
            FallBridge();
        else
            DisableBridge();

        if (nextBridgeCollider.isTrigger && !nextInstance)
        {
            levelManagerInstance.bridgesInstanciePosition = new Vector3(endPosition.position.x, startHeight,endPosition.position.z);
            levelManagerInstance.SpawnBridge();
            nextInstance = true;
        }

    }

    private void FallBridge()
    {
        position.x = transform.position.x;
        position.z = transform.position.z;
        if (transform.position.y > 0)
        {
            position.y -= fallSpeed * Time.deltaTime;
            transform.position = position;
        }
        else if (transform.position.y < 0)
        {
            position.y = 0;
            transform.position = position;
        }
    }

     private void DisableBridge()
     {
        if (transform.position.y > endHeight)
        {
            position = transform.position;
            position.y -= fallSpeed * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            gameObject.SetActive(false);
        }
     }
}
