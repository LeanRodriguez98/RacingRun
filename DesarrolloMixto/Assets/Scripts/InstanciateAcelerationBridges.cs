using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateAcelerationBridges : MonoBehaviour {
    public GameObject aceletarionBridge;
    public int cantBridgesToInstance;
    public float intanciateTime;
    private float auxIntanciateTime;
    private GameManager gamemanagerInstance;
    // Use this for initialization
    void Start () {
        auxIntanciateTime = 0;
        gamemanagerInstance = GameManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (cantBridgesToInstance >= 0)
        {
            auxIntanciateTime += Time.deltaTime;

            if (auxIntanciateTime >= intanciateTime)
            {
                auxIntanciateTime = 0;
                cantBridgesToInstance--;
                Instantiate(aceletarionBridge, gamemanagerInstance.instancePosition, Quaternion.Euler(gamemanagerInstance.instanceRotation));
                gamemanagerInstance.instanceRotation.y += aceletarionBridge.GetComponent<BridgeData>().EndBridgeRotation;
            }
        }
	}
}
