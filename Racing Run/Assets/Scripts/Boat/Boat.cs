using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public float maxDistanceToPlayer;
    public float offsetBackToReturnTolayer;
    public float TimerToChangeDirection;
    private float auxRotationSpeed = 0;

    private Car carInstance;
    void Start ()
    {
        carInstance = Car.instance;
        ChangeRotationSpeed();
    }
	
	void Update () {
        transform.Rotate(0, Time.deltaTime * auxRotationSpeed, 0);
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Vector3.Distance(carInstance.transform.position,transform.position) > maxDistanceToPlayer)
        {
            Vector3 p = Vector3.zero;
            p = carInstance.transform.position - carInstance.transform.forward * offsetBackToReturnTolayer;
            p.y = transform.position.y;
            transform.position = p;

        }
    }

    public void ChangeRotationSpeed()
    {
        auxRotationSpeed = Random.Range(-rotationSpeed, rotationSpeed);
        Invoke("ChangeRotationSpeed", TimerToChangeDirection);
    }
}
