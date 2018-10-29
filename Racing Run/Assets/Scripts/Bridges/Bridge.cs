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
    private Vector3 position;
    private Vector3 rotation;
    private LevelManager levelManagerInstance;

    private void Start()
    {
        levelManagerInstance = LevelManager.instance;
    }

    void OnEnable () {
        if (levelManagerInstance != null)
        {
            position = levelManagerInstance.bridgesInstanciePosition;
            position.y = startHeight;
            transform.position = position;
            rotation = levelManagerInstance.bridgesInstancieRotation;
            transform.rotation = Quaternion.Euler(rotation);
            endBridgeCollider.isTrigger = false;
            nextBridgeCollider.isTrigger = false;
        }
    }
	
	void Update () {
        if (!endBridgeCollider.isTrigger)
            FallBridge();
        else
            DisableBridge();

        if (nextBridgeCollider.isTrigger)
        {
            levelManagerInstance.bridgesInstanciePosition = endPosition.position;
            levelManagerInstance.SpawnBridge();
        }

    }

    private void FallBridge()
    {
        if (transform.position.y > levelManagerInstance.bridgesInstanciePosition.y)
        {
            position.y -= fallSpeed * Time.deltaTime;
            transform.position = position;
        }
        else if (transform.position.y < levelManagerInstance.bridgesInstanciePosition.y)
        {
            position.y = levelManagerInstance.bridgesInstanciePosition.y;
            transform.position = position;
        }
    }

     private void DisableBridge()
     {
        if (transform.position.y > endHeight)
        {
            position.y -= fallSpeed * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            gameObject.SetActive(false);
        }
     }
}
