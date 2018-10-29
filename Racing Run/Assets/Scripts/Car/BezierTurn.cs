using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTurn : MonoBehaviour {

    private float t = 0;
    private Vector3 p;
    private float carRotation;
    [HideInInspector] public Car carInstance;
    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public int CurveSmoothness;
    public enum Direction {Left, Right};
    public Direction direction;
    private void Awake()
    {
        carInstance = Car.instance;
    }

    public void OnTriggerStay(Collider other)
    {
   
         if (other.tag == "CarCenter")
         {
            carInstance.bezierTurn = this;
            p0.transform.position = Car.instance.transform.position;
            if(direction == Direction.Left)
            carRotation = carInstance.transform.rotation.eulerAngles.y - 90;
            if(direction == Direction.Right)
            carRotation = carInstance.transform.rotation.eulerAngles.y + 90;
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

    private Vector3 CalculatePoint()
    {
        p = ((1 - t) * (1 - t) * (1 - t)) * p0.transform.position + 3 * ((1 - t) * (1 - t)) *
            t * p1.transform.position + 3 * (1 - t) * (t * t) * p2.transform.position +
            (t * t * t) * p3.transform.position;
        return p;
    }


    private void TurnOn()
    {
        if (carInstance.states == Car.States.Forward)
            carInstance.states = Car.States.Turn;
    }

    private void TurnOff()
    {
        carInstance.states = Car.States.Forward;
    }

    public Vector3 GetFixedRotation()
    {
        Vector3 r = carInstance.transform.rotation.eulerAngles;
        r.y = carRotation;
        return r;
    }

   

}
