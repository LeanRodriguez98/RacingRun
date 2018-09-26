using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] Terrains;
    public GameObject OptionsPanel;
    public Vector3 instancePosition;
    public Vector3 instanceRotation;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    // Use this for initialization
    
    void Start () {
        Application.targetFrameRate = 60;
        instanceRotation = Vector3.zero;
        instancePosition.y = 5;
	}
	
	// Update is called once per frame
	void Update () {
    

        
    }

    public void Instanciator(int index)
    {
        
        Instantiate(Terrains[index], instancePosition, Quaternion.Euler(instanceRotation));
        instanceRotation.y += Terrains[index].GetComponent<BridgeData>().EndBridgeRotation;
        

        if (instanceRotation.y <= -360)
        {
            instanceRotation.y += 360;
        }
        if (instanceRotation.y >= 360)
        {
            instanceRotation.y -= 360;

        }
    }
}
