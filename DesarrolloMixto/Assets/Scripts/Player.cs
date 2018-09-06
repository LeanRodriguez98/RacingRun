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
    public TurnCollider leftCollider;
    public TurnCollider rightCollider;
    private Quaternion rotation;
    private Quaternion auxRotation;

    private Rigidbody rb;
    private float time;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotation = auxRotation = transform.rotation;
    }

    private void Update()
    {
        time += Time.deltaTime * 0.01f;
        rb.AddForce(-transform.right * time);

        if (leftCollider.Collider())
        {
            auxRotation.y -= 90;
        }

        if (rightCollider.Collider())
        {
            auxRotation.y += 90;

        }

        if (rotation.y < auxRotation.y)
        {
            Debug.Log(1);
            rotation.y += Time.deltaTime * 5;
            transform.rotation = rotation;
        }

        if (rotation.y > auxRotation.y)
        {
            Debug.Log(1);

            rotation.y += Time.deltaTime * 5;
            transform.rotation = rotation;
        }

        if (InputManager.instance.Fire())
        {
            WeaponLeft.Fire();
            WeaponRight.Fire();
        }
    }
}
