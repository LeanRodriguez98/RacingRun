using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateAcelerationBridges : MonoBehaviour {
    public GameObject aceletarionBridge;

    public int cantBridgesToInstance;
    public float intanciateTime;
    private float auxIntanciateTime;
    private GameManager gamemanagerInstance;
    private GameObject firstBridge;
    // Use this for initialization
    void Start () {
        auxIntanciateTime = 0;
        gamemanagerInstance = GameManager.instance;
        firstBridge = gamemanagerInstance.Terrains[Random.Range(0, gamemanagerInstance.Terrains.Length)];
    }
	
	// Update is called once per frame
	void Update () {
       
            auxIntanciateTime += Time.deltaTime;

            if (auxIntanciateTime >= intanciateTime)
            {
                if (cantBridgesToInstance > 0)
                {
                    Instanciator(aceletarionBridge);
                }
                else if (cantBridgesToInstance == 0)
                {
                    Instanciator(firstBridge);
                }
            }
        
    }

    private void Instanciator(GameObject go)
    {
        auxIntanciateTime = 0;
        cantBridgesToInstance--;
        Instantiate(go, gamemanagerInstance.instancePosition, Quaternion.Euler(gamemanagerInstance.instanceRotation));
        gamemanagerInstance.instanceRotation.y += go.GetComponent<BridgeData>().EndBridgeRotation;
    }
}
