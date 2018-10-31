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
    [HideInInspector]public float metersTraveled;
    private ObjectPooler objectPoolerInstance;
    [HideInInspector] public int nuts;
    public float startDelay;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        objectPoolerInstance = ObjectPooler.instance;
        metersTraveled = 0;
    }


    void Update () {
        if (startDelay < 0)
        {


            if (speed < maxSpeed)
                speed += Time.deltaTime * ascelerationMultipler;

            metersTraveled += speed * Time.deltaTime;

            switch (states)
            {
                case States.Forward:
                    transform.position += transform.forward * speed * Time.deltaTime;
                    transform.Translate(Input.acceleration.x * speed * Time.deltaTime, 0, 0);
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.Translate(0.5F * speed * Time.deltaTime, 0, 0);
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        transform.Translate(-0.5F * speed * Time.deltaTime, 0, 0);
                    }
                    break;
                case States.Turn:
                    Vector3 pos = Vector3.zero;
                    pos.x = bezierTurn.CalculateCubicBezierPoint(speed).x;
                    pos.y = transform.position.y;
                    pos.z = bezierTurn.CalculateCubicBezierPoint(speed).z;
                    transform.position = pos;
                    transform.LookAt(new Vector3(bezierTurn.LookAtPoint().x, transform.position.y, bezierTurn.LookAtPoint().z));
                    if (states == States.Forward)
                    {
                        //transform.eulerAngles = bezierTurn.GetFixedRotation();
                        FixCarAngle();
                        bezierTurn = null;
                    }
                    break;
                default:
                    break;
            }

            if (life <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            startDelay -= Time.deltaTime;
        }
    }


    private void FixCarAngle()
    {
        float rotationY = transform.rotation.eulerAngles.y;
        Vector3 fixedRotation = transform.rotation.eulerAngles;
        if ((rotationY % 90) != 0)
        {
            if (rotationY >= 45 && rotationY < 135)
            {
                fixedRotation.y = 90;
            }
            else if (rotationY >= 135 && rotationY < 225)
            {
                fixedRotation.y = 180;
            }
            else if (rotationY >= 225 && rotationY < 315)
            {
                fixedRotation.y = 270;
            }
            else
            {
                fixedRotation.y = 0;
            }

            transform.eulerAngles = fixedRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightArrow")
        {
            //animations.SetTrigger("TurnRight");
            objectPoolerInstance.SpawnForPool("BezierRight", transform.position + this.transform.forward, Quaternion.Euler(0, transform.eulerAngles.y - 180, 0));
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "LeftArrow")
        {
            //animations.SetTrigger("TurnLeft");
            objectPoolerInstance.SpawnForPool("BezierLeft", transform.position + this.transform.forward, Quaternion.Euler(0, transform.eulerAngles.y - 180, 0));
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Nut")
        {
            other.gameObject.SetActive(false);
            nuts++;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.SetActive(false);
            life -= 10;
            Handheld.Vibrate();
        }
        if (other.gameObject.tag == "LifePickUp")
        {
            other.gameObject.SetActive(false);
            if (life < 100)
            {
                life += 10;
            }
        }
        if (other.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);
            
        }
    }
} 
