using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instancie;
    public GameObject[] Terrains;
    public Vector3 instancePosition;
    private Vector3 instanceRotation;
    // Use this for initialization
    void Awake()
    {
        instancie = this;
    }
    void Start () {
        instanceRotation = Vector3.zero;
        instancePosition.y = 5;
	}
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown(KeyCode.Alpha1))
       {
            Instantiate(Terrains[0], instancePosition, Quaternion.Euler(instanceRotation));          
       }
       else if (Input.GetKeyDown(KeyCode.Alpha2))
       {

            Instantiate(Terrains[1], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y -= 90; 
       }
       else if (Input.GetKeyDown(KeyCode.Alpha3))
       {

            Instantiate(Terrains[2], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y += 90;
       }

        if (instanceRotation.y <= -360 || instanceRotation.y >= 360)
        {
            instanceRotation.y = 0;
        }
    }
}
