using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {
    private Vector3 position;
    private Vector3 rotation;
    private LevelManager levelManagerInstance;
    private bool nextInstance;

    public enum NextBridgeType
    {
        Game,
        Tutorial
    };
    [Header("SpawnSettings")]
    [Space(10)]
    public float startHeight;
    public float endHeight;
    public float fallSpeed;
    [Header("NextBridgeSettings")]
    [Space(10)]
    public NextBridgeType nextBridgeType;
    public Transform endPosition;
    public float exitRotationY;
    [Header("TriggerColliders")]
    [Space(10)]
    public TriggerCollider endBridgeCollider;
    public TriggerCollider nextBridgeCollider;  
    [Header("Railings")]
    [Space(10)]
    public bool haveRailings;
    public GameObject[] railings;

    private void Start()
    {
        levelManagerInstance = LevelManager.instance;
    }

    void OnEnable () {
  
        position.y = startHeight;       
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

       
    }

    

    void Update () {
        if (!endBridgeCollider.isTrigger)
            FallBridge();
        else
            DisableBridge();

        if (nextBridgeCollider.isTrigger && !nextInstance)
        {
            
            levelManagerInstance.bridgesInstanciePosition = new Vector3(endPosition.position.x, startHeight,endPosition.position.z);
            levelManagerInstance.bridgesInstancieRotation.y += exitRotationY;
            if (nextBridgeType == NextBridgeType.Game)
                levelManagerInstance.SpawnBridge();
            else if (nextBridgeType == NextBridgeType.Tutorial)            
                levelManagerInstance.SpawnTutorialBridge();
            
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
