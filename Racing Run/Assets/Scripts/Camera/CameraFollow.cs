using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    private Transform target;
    public float distance;
    public float height;
    public float damping;
    public bool smoothRotation;
    public bool followBehind;
    public float rotationDamping;
    public Car carInstance;


    private void Start()
    {
        carInstance = Car.instance;
        target = carInstance.transform;
    }



    void LateUpdate()
    {
        if (carInstance != null)
        {
            Vector3 wantedPosition;
            if (followBehind)
                wantedPosition = target.TransformPoint(0, height, -distance);
            else
                wantedPosition = target.TransformPoint(0, height, distance);

            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    
            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else
                transform.LookAt(target, target.up);
        }
    }
}