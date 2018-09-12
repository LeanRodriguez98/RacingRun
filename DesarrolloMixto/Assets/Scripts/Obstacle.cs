using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public int life;
    private Player playerInstance;
	// Use this for initialization
	void Start () {
        playerInstance = Player.instance;

    }
	
	// Update is called once per frame
	void Update () {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnMouseDown()
    {
        playerInstance.ObstacleToShoot = this.gameObject;
    }
}
