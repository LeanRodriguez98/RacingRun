using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] Terrains;
    public Vector3 instancePosition;
    public Vector3 instanceRotation;
    [HideInInspector] public int auxInstance = -1;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    #endregion
    // Use this for initialization
    
    void Start () {
        instanceRotation = Vector3.zero;
        instancePosition.y = 5;
	}
	
	// Update is called once per frame
	void Update () {
    

        if (auxInstance != -1)
        {
            Instantiate(Terrains[auxInstance], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y += Terrains[auxInstance].GetComponent<BridgeData>().EndBridgeRotation;
            auxInstance = -1;
        }

        if (instanceRotation.y <= -360 )
        {
            instanceRotation.y += 360;
        }
        if (instanceRotation.y >= 360)
        {
            instanceRotation.y -= 360;

        }
    }
}
