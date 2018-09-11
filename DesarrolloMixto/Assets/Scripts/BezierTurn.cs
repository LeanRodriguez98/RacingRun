using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTurn : MonoBehaviour {

    private float t = 0;
    private Vector3 p;
    public Player player;
    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public int CurveSmoothness;

    private void Awake()
    {
        player = Player.instance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarCenter")
        {
            Player.instance.bezierTurn = this;
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
    }

    private Vector3 CalculatePoint()
    {
        p = ((1 - t) * (1 - t) * (1 - t)) * p0.transform.position + 3 * ((1 - t) * (1 - t)) * t * p1.transform.position + 3 * (1 - t) * (t * t) * p2.transform.position + (t * t * t) * p3.transform.position;
        return p;
    }
   
}
