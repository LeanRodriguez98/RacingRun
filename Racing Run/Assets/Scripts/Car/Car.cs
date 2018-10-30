using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public int life;
    public float speed;
    public float maxSpeed;
    public float ascelerationMultipler;
    public enum States {Forward,Turn};
    public States states = States.Forward;
    public static Car instance;
    public BezierTurn bezierTurn;
    private ObjectPooler objectPoolerInstance;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        objectPoolerInstance = ObjectPooler.instance;

    }


    void Update () {
        if (speed < maxSpeed)
            speed += Time.deltaTime * ascelerationMultipler;
        switch (states)
        {
            case States.Forward:                
                transform.position += transform.forward * speed * Time.deltaTime;
                transform.Translate(Input.acceleration.x * speed * Time.deltaTime, 0, 0);
                break;
            case States.Turn:
                Vector3 pos = Vector3.zero;
                pos.x = bezierTurn.CalculateCubicBezierPoint(speed).x;
                pos.y = transform.position.y;
                pos.x = bezierTurn.CalculateCubicBezierPoint(speed).z;
                transform.position = pos;
                transform.LookAt(new Vector3(bezierTurn.LookAtPoint().x, transform.position.y, bezierTurn.LookAtPoint().z));
                if (states == States.Forward)
                {
                    transform.eulerAngles = bezierTurn.GetFixedRotation();
                    bezierTurn = null;
                }                
                break;
            default:
                break;
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightArrow")
        {
            //animations.SetTrigger("TurnRight");
            objectPoolerInstance.SpawnForPool("BezierRight", transform.position + this.transform.forward * 4, Quaternion.Euler(0, transform.eulerAngles.y, 0));
        }
        if (other.gameObject.tag == "LeftArrow")
        {
            //animations.SetTrigger("TurnLeft");
            objectPoolerInstance.SpawnForPool("BezierLeft", transform.position + this.transform.forward * 4, Quaternion.Euler(0, transform.eulerAngles.y, 0));

        }
        if (other.gameObject.tag == "Nut")
        {
            other.gameObject.SetActive(false);
        }
    }
} 
