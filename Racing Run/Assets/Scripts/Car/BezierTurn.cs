using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTurn : MonoBehaviour {

    private Car carInstance;
    private float t = 0;
    private Vector3 p;
    private float carRotation;
    public enum Direction { Left, Right };

    [Header("BezierPoints")]
    [Space(10)]
    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    [Header("CurveSmoothness")]
    [Space(10)]
    public int CurveSmoothness;
    [Header("DirectionArrow")]
    [Space(10)]
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
            TurnOn();
         }
    }
    private void OnEnable()
    {
        t = 0;
    }
    public Vector3 CalculateCubicBezierPoint( float speed)
    {
        if (t >= 1)
            TurnOff();
        p = CalculatePoint();
        t += Time.deltaTime * (speed / CurveSmoothness);       
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
        gameObject.SetActive(false);
    }


}
