using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterial : MonoBehaviour {
    [HideInInspector] public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = PlayerStats.instance;

    }
    void Start () {

        if (playerStats != null)
        gameObject.GetComponent<Renderer>().material = playerStats.currentMaterial;
    }
	
	
}
