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
    public TurnInput turnInput;
    public enum Direction {left,right};
    public Direction directionTurn;

    private void Awake()
    {
        player = Player.instance;
        turnInput = TurnInput.instance;
    }

    public void OnTriggerStay(Collider other)
    {
        //che player, estoy colisionando con vos
      /*  if (other.tag == "CarCenter")
        {
            Debug.Log("now");

            Debug.Log("directionTurn: " + directionTurn);
        }*/

        if ((directionTurn == Direction.left && turnInput.TurnLeft()) || (directionTurn == Direction.right && turnInput.TurnRight()))
        {
            if (other.tag == "CarCenter")
            {
                //Debug.Log("HELLO DUDE " + directionTurn);

                Player.instance.bezierTurn = this;
                p0.transform.position = Player.instance.transform.position;
                TurnOn();
            }
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
