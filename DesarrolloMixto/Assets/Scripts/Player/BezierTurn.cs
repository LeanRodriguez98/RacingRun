using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTurn : MonoBehaviour {

    private float t = 0;
    private Vector3 p;
    public Player playerInstance;
    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public int CurveSmoothness;


    private void Awake()
    {
        playerInstance = Player.instance;
    }

    public void OnTriggerStay(Collider other)
    {
   
            if (other.tag == "CarCenter")
            {

                Player.instance.bezierTurn = this;
                p0.transform.position = Player.instance.transform.position;
                TurnOn();
            }
        
    }

    public Vector3 CalculateCubicBezierPoint( float speed)
    {
        if (t >= 1)
            TurnOff();
        p = CalculatePoint();
        t += Time.deltaTime * speed / CurveSmoothness;       
        return p;
    }

    public Vector3 LookAtPoint()
    {
        p = CalculatePoint();
        return p;
    }
    private void TurnOn()
    {
        Player.instance.State = Player.States.Turn;
    }

    private void TurnOff()
    {
        Player.instance.State = Player.States.Forward;
        Destroy(gameObject, 1);
    }

    private Vector3 CalculatePoint()
    {
        p = ((1 - t) * (1 - t) * (1 - t)) * p0.transform.position + 3 * ((1 - t) * (1 - t)) * t * p1.transform.position + 3 * (1 - t) * (t * t) * p2.transform.position + (t * t * t) * p3.transform.position;
        return p;
    }
   
}
