using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    private ObjectPooler objectPoolerInstance;
    private GameSaveManager gameSaveManagerInstance;
    private AudioManager audioManagerInstance;
    private Rigidbody rb;
    private Vector3 accelerationInput;

    private Ray floorDistanceRay;
    private RaycastHit floorDistanceRaycastHit;
    [HideInInspector] public float floorDistance;

    public enum States { Forward, Turn };
    [HideInInspector]public States states = States.Forward;
    [HideInInspector] public float speed;
    [HideInInspector] public bool nitro;
    [HideInInspector] public float metersTraveled;
    [HideInInspector] public int nuts;
    [HideInInspector] public BezierTurn bezierTurn;
    [HideInInspector] public bool InmortalCheat = false;
    [HideInInspector] public bool tutorialEnded = false;

    public static Car instance;
    [Header("Stats")]
    [Space(10)]
    public SO_PlayerStats soPlayerStats;
    [Header("Life")]
    [Space(10)]
    public int life = 3;
    public float flickingTime;
    private float auxFlickingTime;
    [Header("SpeedSettings")]
    [Space(10)]
    public float maxSpeed;
    public float ascelerationMultipler;
    public float horizontalSpeedMultipler;
    public float startDelay;
    [Header("NitroSettings")]
    [Space(10)]
    public float nitroMaxSpeed;
    public float nitroAscelerationMultipler;
    public float maxNitroAcumulation;
    public float nitroDesacumulationMultipler;
    public float nitroAcumulationMultipler;
    public float minNitroAcumulationToEnabled;
    [HideInInspector] public float nitroAcumulation;
    [Header("JumpSettings")]
    [Space(10)]
    public float jumpChargeTime;
    private float auxJumpChargeTime;
    public int jumpForce;
    [Header("Animations")]
    [Space(10)]
    public Animator animations;
    [Header("CarParts")]
    [Space(10)]
    public Renderer[] meshParts;
    public RalingsColliders leftCollider;
    public RalingsColliders rightCollider;
    public TrailRenderer[] skildMarks;
    public CarLight[] lights;
    [Header("Particles")]
    [Space(10)]
    public ParticleSystem[] tailpipeParticles;    
    public ParticleSystem[] wheelParticles;
    public ParticleSystem[] nitroParticles;
    public float tailpipeParticlesSpeedMultipler;
    public float tailpipeParticlesMinTimeOfLife;
    [Header("AudioClips")]
    [Space(10)]
    [Header("       AudioClips - EngineSounds")]
    [Space(5)]
    public AudioManager.Clip engineStart;
    public AudioManager.Clip engineAcceleration;
    public AudioManager.Clip engineOnTurn;
    public AudioManager.Clip engineOnAir;
    public AudioManager.Clip engineSlowdown;
    public AudioManager.Clip engineTopSpeed;
    [Header("       AudioClips - OnCrashSounds")]
    [Space(5)]
    public AudioManager.Clip[] nutsSounds;
    public AudioManager.Clip HitConeSound;
    public AudioManager.Clip HitBarrierSound;
    public AudioManager.Clip HitCrashBoxSound;
    public AudioManager.Clip HitStopSignalSound;
    public AudioManager.Clip TakeToolBoxSound;
    public AudioManager.Clip WaterSplashSound;
    private void Awake()
    {
        instance = this;
    }

    public void PlayEngineTopSpeedSound()
    {
        audioManagerInstance.PlayLoopSound(engineTopSpeed.clip, engineTopSpeed.Volume);
    }

    public void PlayEngineSlowdownSound()
    {
        audioManagerInstance.StopLoopSound();
        audioManagerInstance.PlayTriggerSound(engineTopSpeed.clip, engineTopSpeed.Volume);
        this.Invoke("PlayTriggerSound", engineAcceleration, engineTopSpeed.clip.length);
    }

    public void PlayTriggerSound(AudioManager.Clip audioClip)
    {
        audioManagerInstance.StopLoopSound();
        audioManagerInstance.PlayTriggerSound(audioClip.clip, audioClip.Volume);
        Invoke("PlayEngineTopSpeedSound", audioClip.clip.length);
    }

  

    void Start () {
        gameSaveManagerInstance = GameSaveManager.instance;
        gameSaveManagerInstance.LoadGame(soPlayerStats);
        objectPoolerInstance = ObjectPooler.instance;
        audioManagerInstance = AudioManager.instance;
        metersTraveled = 0;
        audioManagerInstance.PlayTriggerSound(engineStart.clip, engineStart.Volume);
        Invoke("PlayEngineTopSpeedSound", engineStart.clip.length);
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
        for (int i = 0; i < nitroParticles.Length; i++)
        {
            nitroParticles[i].Stop();
        }
    }


    void Update () {
        for (int i = 0; i < tailpipeParticles.Length; i++)
        {
            tailpipeParticles[i].startLifetime = speed * tailpipeParticlesSpeedMultipler;

            if (tailpipeParticles[i].startLifetime < tailpipeParticlesMinTimeOfLife)
                tailpipeParticles[i].startLifetime = tailpipeParticlesMinTimeOfLife;
            
        }

        floorDistanceRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(floorDistanceRay, out floorDistanceRaycastHit))
            floorDistance = floorDistanceRaycastHit.distance;

        if (startDelay < 0)
        {
            //Debug.Log(nitro + " " + nitroAcumulation);
            if (nitro)
            {
                if (speed < nitroMaxSpeed)
                    speed += Time.deltaTime * nitroAscelerationMultipler;

                nitroAcumulation -= Time.deltaTime * nitroDesacumulationMultipler;

                if (nitroAcumulation <= 0)
                {
                    nitro = false;
                    for (int i = 0; i < nitroParticles.Length; i++)
                    {
                        nitroParticles[i].Stop();
                    }
                }
            }
            else
            {
                if (speed > maxSpeed)
                {
                    speed -= Time.deltaTime * nitroAscelerationMultipler;
                    if (speed < maxSpeed)
                        speed = maxSpeed;                    
                }
                if (speed < maxSpeed)
                    speed += Time.deltaTime * ascelerationMultipler;


                if (nitroAcumulation < maxNitroAcumulation)
                    nitroAcumulation += Time.deltaTime * nitroAcumulationMultipler;

            }


            for (int i = 0; i < wheelParticles.Length; i++)
            {
                if (wheelParticles[i].startLifetime > 0)
                {
                    wheelParticles[i].startLifetime -= Time.deltaTime * 2;
                }
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Z))
            {
                speed = maxSpeed = 0;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                speed = maxSpeed = 15;
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

                    animations.SetFloat("Inclination",Input.acceleration.x);


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

            for (int i = 0; i < wheelParticles.Length; i++)
            {
                wheelParticles[i].startLifetime += Time.deltaTime;
            }

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
            PlayTriggerSound(engineOnAir);
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

    public void EnableNitro()
    {
        if (nitroAcumulation > minNitroAcumulationToEnabled)
        {
            nitro = true;
            for (int i = 0; i < nitroParticles.Length; i++)
            {
                nitroParticles[i].Play(); 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightArrow")
        {
            animations.SetTrigger("TurnRight");
            objectPoolerInstance.SpawnForPool("BezierRight", transform.position + this.transform.forward, Quaternion.Euler(0, transform.eulerAngles.y - 180, 0));
            other.gameObject.transform.parent.gameObject.SetActive(false);
            for (int i = 0; i < skildMarks.Length; i++)
            {
                skildMarks[i].emitting = true;
            }
            PlayTriggerSound(engineOnTurn);
        }
        if (other.gameObject.tag == "LeftArrow")
        {
            animations.SetTrigger("TurnLeft");
            objectPoolerInstance.SpawnForPool("BezierLeft", transform.position + this.transform.forward, Quaternion.Euler(0, transform.eulerAngles.y - 180, 0));
            other.gameObject.transform.parent.gameObject.SetActive(false);

            for (int i = 0; i < skildMarks.Length; i++)
            {
                skildMarks[i].emitting = true;
            }
            PlayTriggerSound(engineOnTurn);

        }
        if (other.gameObject.tag == "Nut")
        {
            int index = Random.Range(0, nutsSounds.Length);
            audioManagerInstance.PlayTriggerSound(nutsSounds[index].clip, nutsSounds[index].Volume);
            other.gameObject.SetActive(false);
            nuts++;
            soPlayerStats.nuts++;
        }
        if (other.gameObject.tag == "Obstacle" )
        {
            if (!InmortalCheat && flickingTime > auxFlickingTime && !nitro)
            {
                life--;
                flickingTime = 0;
                PlayEngineSlowdownSound();
                switch (other.gameObject.name)
                {
                    case "Cone(Clone)":
                        audioManagerInstance.PlayTriggerSound(HitConeSound.clip, HitConeSound.Volume);
                        break;
                    case "Barricade(Clone)":
                        audioManagerInstance.PlayTriggerSound(HitBarrierSound.clip,HitBarrierSound.Volume);
                        break;
                    case "BarrierCollider":
                    case "CrashBox(Clone)":
                        audioManagerInstance.PlayTriggerSound(HitCrashBoxSound.clip,HitCrashBoxSound.Volume);
                        break;
                    case "TrainBarrierRight(Clone)":
                    case "TrainBarrierLeft(Clone)":
                    case "StopSignal(Clone)":
                        audioManagerInstance.PlayTriggerSound(HitStopSignalSound.clip,HitStopSignalSound.Volume);
                        break;
                    default:
                        Debug.LogWarning("The obstacle " + other.gameObject.name + " have not assigned a sound when the car crash whith them");
                        break;
                }
            }
            animations.SetTrigger("Crash");
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }
        if (other.gameObject.tag == "LifePickUp")
        {
            other.gameObject.SetActive(false);
            audioManagerInstance.PlayTriggerSound(TakeToolBoxSound.clip, TakeToolBoxSound.Volume);
            if (life < 3)            
                life++;

            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i].mesh.enabled == false)
                {
                    lights[i].Repair();
                    break;
                }
            }
        }
        if (other.gameObject.tag == "Water")
        {
            audioManagerInstance.PlayTriggerSound(WaterSplashSound.clip, WaterSplashSound.Volume);
            life = 0;            
        }

        if (other.gameObject.tag == "TutorialEnd")
        {
            tutorialEnded = true;
        }
    }


    private void OnDisable()
    {
        if (audioManagerInstance != null)
        {
            audioManagerInstance.SilenceSounds();
            CancelInvoke();
        }
    }

} 


