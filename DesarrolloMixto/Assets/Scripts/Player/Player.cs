using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public enum States { Forward, Turn, Crashed };
    public enum CrashStates { Up, Stay, Down };
    public States State;
    public CrashStates CrashState;
    [HideInInspector] public BezierTurn bezierTurn;
    [HideInInspector] public int nuts;
    public float MaxSpeed;
    public float AcelerationMultipler;
    public int life;
    #region Singleton
    public static Player instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    [HideInInspector]public float Speed;

    private int auxLife;
    private Vector3 crashRotation;
    private bool stuned;
    float StayTime;
    public Animator animations;

    public float startDelay;
    
    private void Start()
    {
        State = States.Forward;
        auxLife = life;
        nuts = 0;
        if (StoreManager.instance != null)
        StoreManager.instance.playerInstance = this;
    }

    private void Update()
    {
        if (startDelay <= 0)
        {
            Movement();
            UpdateLifeBar();
        }
        else
        {
            startDelay -= Time.deltaTime;
        }
    }

    private void Movement()
    {
        switch (State)
        {

            case States.Forward:
                if (Speed < MaxSpeed)
                    Speed += Time.deltaTime * AcelerationMultipler;
                transform.position += transform.forward * Speed * Time.deltaTime;
                transform.Translate(Input.acceleration.x * Speed *Time.deltaTime,0,0);

                break;
            case States.Turn:
                if (Speed < MaxSpeed)
                    Speed += Time.deltaTime * AcelerationMultipler;
                Vector3 pos;
                pos.x = bezierTurn.CalculateCubicBezierPoint(Speed).x;
                pos.y = transform.position.y;
                pos.z = bezierTurn.CalculateCubicBezierPoint(Speed).z;
                transform.position = pos;
                transform.LookAt(new Vector3(bezierTurn.LookAtPoint().x , transform.position.y , bezierTurn.LookAtPoint().z));     
                if(State == States.Forward)
                    FixCarAngle();
                break;
           
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

 

    private void UpdateLifeBar()
    {
        if (life > auxLife)        
            life = auxLife;
        if (life <= 0)
            gameObject.SetActive(false);

        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LifePickUp")
        {
            life += 10;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            life -= 10;

            animations.SetTrigger("Crash");
            other.gameObject.SetActive(false);
            Vibrator.Vibrate(5);

        }

        if (other.gameObject.tag == "Nut")
        {
            nuts++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "TurnRight")
        {
            animations.SetTrigger("TurnRight");
        }

        if (other.gameObject.tag == "TurnLeft")
        {
            animations.SetTrigger("TurnLeft");
        }

    }

}
