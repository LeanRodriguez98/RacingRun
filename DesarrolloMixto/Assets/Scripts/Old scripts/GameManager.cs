using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instancie;
    public GameObject[] Terrains;
    public Vector3 instancePosition;
    private Vector3 instanceRotation;
    [HideInInspector] public int auxInstance = -1;
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
       if (auxInstance == 0)
       {
            Instantiate(Terrains[0], instancePosition, Quaternion.Euler(instanceRotation));          
       }
       else if (auxInstance == 1)
       {

            Instantiate(Terrains[1], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y -= 90; 
       }
       else if (auxInstance == 2)
       {

            Instantiate(Terrains[2], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y += 90;
       }
        else if (auxInstance == 3)
        {

            Instantiate(Terrains[3], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y -= 180;
        }
        else if (auxInstance == 4)
        {

            Instantiate(Terrains[4], instancePosition, Quaternion.Euler(instanceRotation));
            instanceRotation.y += 180;


        }

        if (auxInstance != -1)
        {
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
