using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    private Transform target;
    private Camera thisCamera;
    private Car carInstance;

    [Header("CameraFollowSettings")]
    [Space(10)]
    public float distance;
    public float height;
    public float damping;
    public float rotationDamping;
    public bool smoothRotation;
    public bool followBehind;

    [Header("OnCarNitroFOVsettings")]
    [Space(10)]
    public float FOVInCameraMultipler;
    public float FOVOutCameraMultipler;

    private void Start()
    {
        carInstance = Car.instance;
        target = carInstance.transform;
        thisCamera = GetComponent<Camera>();
    }



    void LateUpdate()
    {
        if (carInstance != null)
        {
            if (carInstance.nitro)
            {
                thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, 90, FOVOutCameraMultipler *  Time.deltaTime);
            }
            else
            {
                thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, 60, FOVInCameraMultipler * Time.deltaTime);
            }

            Vector3 wantedPosition;
            float totalHeight = Mathf.Lerp(height, height + carInstance.floorDistance, Time.deltaTime);
            float totalDistance = Mathf.Lerp(distance, distance - carInstance.floorDistance, Time.deltaTime);
            float totalDamping = Mathf.Lerp(damping, damping + carInstance.floorDistance, Time.deltaTime);
            if (followBehind)
                wantedPosition = target.TransformPoint(0, totalHeight, -totalDistance);
            else
                wantedPosition = target.TransformPoint(0, totalHeight, totalDistance);

            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * totalDamping);
    
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