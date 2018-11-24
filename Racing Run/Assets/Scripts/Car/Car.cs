using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public int life = 3;
    [HideInInspector]public float speed;
    public float maxSpeed;
    public float ascelerationMultipler;
    public enum States {Forward,Turn};
    public States states = States.Forward;
    public static Car instance;
    [HideInInspector] public BezierTurn bezierTurn;
    [HideInInspector] public float metersTraveled;
    private ObjectPooler objectPoolerInstance;
    [HideInInspector] public int nuts;
    public SO_PlayerStats soPlayerStats;
    public float startDelay;
    public Animator animations;
    public int jumpForce;
    private Rigidbody rb;
    public float horizontalSpeedMultipler;
    public Renderer[] meshParts;
    [HideInInspector] public bool tutorialEnded = false;
    public bool InmortalCheat = false;
    public float jumpChargeTime;
    private float auxJumpChargeTime;
    public RalingsColliders leftCollider;
    public RalingsColliders rightCollider;
    private GameSaveManager gameSaveManagerInstance;
    public float flickingTime;
    private float auxFlickingTime;
    public TrailRenderer[] skildMarks;
    private Vector3 accelerationInput;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        gameSaveManagerInstance = GameSaveManager.instance;
        gameSaveManagerInstance.LoadGame(soPlayerStats);
        objectPoolerInstance = ObjectPooler.instance;
        metersTraveled = 0;
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < meshParts.Length; i++)
        {
            meshParts[i].material = Resources.Load<Material>(soPlayerStats.materialName);
        }
        auxJumpChargeTime = jumpChargeTime;
        auxFlickingTime = flickingTime;
        for (int i = 0; i < skildMarks.Length; i++)
        {
            skildMarks[i].emitting = false;
        }
    }


    void Update () {
        if (startDelay < 0)
        {


            if (speed < maxSpeed)
                speed += Time.deltaTime * ascelerationMultipler;
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Z))
            {
                speed = maxSpeed = 0;
            }
#endif
            metersTraveled += speed * Time.deltaTime;

            switch (states)
            {
                case States.Forward:
                    transform.position += transform.forward * speed * Time.deltaTime;

                        FixCarAngle();

#if UNITY_ANDROID
                    if (Input.acceleration.x > 0 && !rightCollider.isTrigger)
                        transform.Translate(Input.acceleration.x * speed * Time.deltaTime, 0, 0);

                    if (Input.acceleration.x < 0 && !leftCollider.isTrigger)
                        transform.Translate(Input.acceleration.x * speed * Time.deltaTime, 0, 0);

                    if (accelerationInput.x > 0)
	                {
                        if (accelerationInput.x < Input.acceleration.x)
                        {
                            animations.SetTrigger("GoCenterToRight");

                        }
                        if (accelerationInput.x > Input.acceleration.x)
                        {
                            animations.SetTrigger("GoRightToCenter");

                        }
                    }

                    if (accelerationInput.x < 0)
                    {
                        if (accelerationInput.x > Input.acceleration.x)
                        {
                            animations.SetTrigger("GoCenterToLeft");

                        }
                        if (accelerationInput.x < Input.acceleration.x)
                        {
                            animations.SetTrigger("GoLeftToCenter");

                        }
                    }

                    accelerationInput = Input.acceleration;
                    
#endif

#if UNITY_EDITOR

                    if (Input.GetKey(KeyCode.RightArrow) && !rightCollider.isTrigger)
                    {
                        transform.Translate(0.5F * speed * Time.deltaTime, 0, 0);
                        accelerationInput.x += Time.deltaTime;

                    }
                    if (Input.GetKey(KeyCode.LeftArrow) && !leftCollider.isTrigger)
                    {
                        transform.Translate(-0.5F * speed * Time.deltaTime, 0, 0);
                        accelerationInput.x -= Time.deltaTime;
                    }

#endif
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
                        for (int i = 0; i < skildMarks.Length; i++)
                        {
                            skildMarks[i].emitting = false;
                        }
                    }
                    break;
                default:
                    break;
            }

            CheckLife();
            if (Input.GetKeyDown(KeyCode.Space))
            Jump();
            if (jumpChargeTime <= auxJumpChargeTime)
            {
                jumpChargeTime += Time.deltaTime;
            }
            if (flickingTime <= auxFlickingTime)
            {
                flickingTime += Time.deltaTime;
            }
        }
        else
        {
            startDelay -= Time.deltaTime;
        }
    }

    public void CheckLife()
    {
        if (life <= 0)
        {
            gameSaveManagerInstance.SaveGame(soPlayerStats);

            gameObject.SetActive(false);
             
        }
    }


    public void Jump()
    {
        if (jumpChargeTime > auxJumpChargeTime)
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpChargeTime = 0;
            animations.SetTrigger("Jump");

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
            animations.SetTrigger("TurnRight");
            objectPoolerInstance.SpawnForPool("BezierRight", transform.position + this.transform.forward, Quaternion.Euler(0, transform.eulerAngles.y - 180, 0));
            other.gameObject.SetActive(false);
            for (int i = 0; i < skildMarks.Length; i++)
            {
                skildMarks[i].emitting = true;
            }
        }
        if (other.gameObject.tag == "LeftArrow")
        {
            animations.SetTrigger("TurnLeft");
            objectPoolerInstance.SpawnForPool("BezierLeft", transform.position + this.transform.forward, Quaternion.Euler(0, transform.eulerAngles.y - 180, 0));
            other.gameObject.SetActive(false);
            for (int i = 0; i < skildMarks.Length; i++)
            {
                skildMarks[i].emitting = true;
            }
        }
        if (other.gameObject.tag == "Nut")
        {
            other.gameObject.SetActive(false);
            nuts++;
            soPlayerStats.nuts++;
        }
        if (other.gameObject.tag == "Obstacle" )
        {
            if (!InmortalCheat && flickingTime > auxFlickingTime)
            {
                life--;
                flickingTime = 0;
            }
            animations.SetTrigger("Crash");
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }
        if (other.gameObject.tag == "LifePickUp")
        {
            other.gameObject.SetActive(false);
            if (life < 3)
            {
                life++;
            }
        }
        if (other.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);
            
        }

        if (other.gameObject.tag == "TutorialEnd")
        {
            tutorialEnded = true;
        }
    }

    
} 
