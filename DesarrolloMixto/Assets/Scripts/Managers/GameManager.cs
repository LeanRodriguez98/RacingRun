using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [System.Serializable]    
    public struct BridgeArray
    {
        public GameObject[] Bridges;
        public int pontsInstancie;
    }

    public BridgeArray[] bridges;

    [HideInInspector] public GameObject[] currentBridges;
    [HideInInspector] public Vector3 instancePosition;
    [HideInInspector] public Vector3 instanceRotation;
    [HideInInspector] public float points = 0;
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
        currentBridges = bridges[0].Bridges;
    }
    #endregion
    // Use this for initialization

    void Start () {
       // QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        instanceRotation = Vector3.zero;
        instancePosition.y = 5;

    }
	
	// Update is called once per frame
	void Update () {   

        for (int i = 0; i < bridges.Length; i++)
        {
            if ((int)points == bridges[i].pontsInstancie)
            {
                currentBridges = bridges[i].Bridges;
            }
        }
        points += Time.deltaTime;

    }

    public void Instanciator()
    {
        Instanciator(Random.Range(0, currentBridges.Length));
    }

    public void Instanciator(int index)
    {        
        Instantiate(currentBridges[index], instancePosition, Quaternion.Euler(instanceRotation));
        instanceRotation.y += currentBridges[index].GetComponent<BridgeData>().EndBridgeRotation;        

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
