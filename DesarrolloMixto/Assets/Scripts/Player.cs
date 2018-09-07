using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

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

    public Weapon WeaponLeft;
    public Weapon WeaponRight;
    public float MaxSpeed;
    private Rigidbody rb;
    private float Speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Speed < MaxSpeed)
        {
            Speed += Time.deltaTime/5;
        }
        rb.AddForce(-transform.right * Speed);
      

        if (InputManager.instance.Fire())
        {
            WeaponLeft.Fire();
            WeaponRight.Fire();
        }
    }
}
