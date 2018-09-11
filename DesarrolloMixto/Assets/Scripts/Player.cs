using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    private Rigidbody rb;
    private float Speed;
    public enum States { Forward, Turn };
    public States State;
    public BezierTurn bezierTurn;
    public Weapon WeaponLeft;
    public Weapon WeaponRight;
    public float MaxSpeed;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        State = States.Forward;
    }

    private void Update()
    {
        Movement();
        Fire();
    }

    private void Movement()
    {
        switch (State)
        {
            case States.Forward:
                if (Speed < MaxSpeed)
                {

                    Speed += Time.deltaTime / 5;
                }
                transform.position += transform.forward * Speed * Time.deltaTime;
                break;
            case States.Turn:
                Vector3 pos;
                pos.x = bezierTurn.CalculateCubicBezierPoint().x;
                pos.y = transform.position.y;
                pos.z = bezierTurn.CalculateCubicBezierPoint().z;
                transform.position = pos;
                transform.LookAt(new Vector3(bezierTurn.LookAtPoint().x , transform.position.y , bezierTurn.LookAtPoint().z));
                
                break;
           
        }        
    }

    private void Fire()
    {
        if (InputManager.instance.Fire())
        {
            WeaponLeft.Fire();
            WeaponRight.Fire();
        }
    }
}
