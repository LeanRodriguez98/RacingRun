using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public enum States { Forward, Turn };
    public States State;
    [HideInInspector]public BezierTurn bezierTurn;
    public Weapon WeaponLeft;
    public Weapon WeaponRight;
    public float MaxSpeed;
    public float AcelerationMultipler;
    public int weaponClip;
    [HideInInspector]public GameObject ObstacleToShoot = null;
    public int life;
    public Slider lifeBar;
    #region Singleton
    public static Player instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    #endregion
    private float Speed;
    private int auxLife;
    private void Start()
    {
        State = States.Forward;
        auxLife = life;
    }

    private void Update()
    {
        Movement();
        Fire();
        UpdateLifeBar();        
    }

    private void Movement()
    {
        switch (State)
        {
            case States.Forward:
                if (Speed < MaxSpeed)
                    Speed += Time.deltaTime * AcelerationMultipler;                
                transform.position += transform.forward * Speed * Time.deltaTime;

                break;
            case States.Turn:
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

    private void Fire()
    {
        if (ObstacleToShoot != null && weaponClip > 0)
        {
            WeaponLeft.Fire(ObstacleToShoot);
            WeaponRight.Fire(ObstacleToShoot);
            weaponClip--;
            ObstacleToShoot = null;
        }
    }

    private void UpdateLifeBar()
    {
        if (life > auxLife)        
            life = auxLife;
        lifeBar.value = life / auxLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AmmoPickUp")
        {
            weaponClip++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "LifePickUp")
        {
            life += 10;
            Destroy(other.gameObject);
        }
    }
}
