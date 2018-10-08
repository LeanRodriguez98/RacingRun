using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public enum States { Forward, Turn, Crashed };
    public enum CrashStates {Up, Stay, Down };
    public States State;
    public CrashStates CrashState;
    [HideInInspector]public BezierTurn bezierTurn;

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
    public float Speed;
    private int auxLife;
    private Vector3 crashRotation;
    private bool stuned;
    float StayTime;
    private Vector3 centredRotation;
    
    private void Start()
    {
        State = States.Forward;
        auxLife = life;
        centredRotation = transform.eulerAngles;
        
    }

    private void Update()
    {
        Movement();
        UpdateLifeBar();    
        

        //swipee
        //tengo algun bezier en connection conmigo
        //chequeo si el lado al que doble es el que quiero.
        //lo muevo

    }

    private void Movement()
    {
        switch (State)
        {
            case States.Crashed:

                switch (CrashState)
                {
                    case CrashStates.Up:
                        crashRotation.x += Time.deltaTime * AcelerationMultipler * 20;
                        if (crashRotation.x > 10)
                        {
                            CrashState = CrashStates.Stay;
                            StayTime=2;
                        }
                        break;
                    case CrashStates.Stay:
                        StayTime -= Time.deltaTime;
                        if (StayTime <= 0)
                        {
                            CrashState = CrashStates.Down;
                        }
                        break;
                    case CrashStates.Down:
                        crashRotation.x -= Time.deltaTime * AcelerationMultipler * 30;
                        if (crashRotation.x < 0)
                        {
                            State = States.Forward;
                            StayTime = 2;
                            CrashState = CrashStates.Up;
                            crashRotation.x = 0;
                        }
                        break;                   
                }

                transform.eulerAngles = crashRotation;
                if(Speed > 1)
                Speed -= Time.deltaTime * AcelerationMultipler * 5;
                transform.position += transform.forward * (Speed/2) * Time.deltaTime;

                break;
            case States.Forward:
                if (Speed < MaxSpeed)
                    Speed += Time.deltaTime * AcelerationMultipler;
                transform.position += transform.forward * Speed * Time.deltaTime;
                transform.Translate(Input.acceleration.x * Speed *Time.deltaTime,0,0);
               /* if ((transform.eulerAngles.y - centredRotation.y) < 10 && Input.acceleration.x > 0)                
                    transform.Rotate(0, Input.acceleration.x * Speed * Time.deltaTime * 30, 0);
                if ((centredRotation.y - transform.eulerAngles.y) < 10 && Input.acceleration.x < 0)
                    transform.Rotate(0, Input.acceleration.x * Speed * Time.deltaTime * 30, 0);
*/
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
           // centredRotation = fixedRotation;
        }
    }

 

    private void UpdateLifeBar()
    {
        if (life > auxLife)        
            life = auxLife;
        if (life <= 0)        
            Destroy(gameObject);

        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LifePickUp")
        {
            life += 10;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            life -= 10;
            State = States.Crashed;
            crashRotation = transform.eulerAngles;
            Destroy(other.gameObject);
        }

    }
}
