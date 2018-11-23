using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public float particleSpeed;
    public float timeToGoToPosition;

    public float lerpTime;
    private float originalLerpTime;
    public float distanceIntensifier;

    public float distanceToDestroy;

    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;

    private Vector3 playerPos;

    private float timer;

    private Car carInstance;
    private Vector3 position;
    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[system.main.maxParticles];
        carInstance = Car.instance;
        originalLerpTime = lerpTime;
        timer = 0;
        position = transform.position;
    }

    private void Start()
    {
    }

   
    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > timeToGoToPosition)
        {
            ParticlesGoToPosition();
        }
        if (position != transform.position)
        {
            timer = 0;
            lerpTime = originalLerpTime;
            position = transform.position;
            system.gravityModifier = -3;
            system.Emit(30);

        }
    }


    private void ParticlesGoToPosition()
    {
        playerPos = carInstance.transform.position - Vector3.up;

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = system.GetParticles(particles);
  

        // Change only the particles that are alive
        for (int i = 0; i < particles.Length; i++)
        {
            float d = Vector3.Distance(particles[i].position, playerPos);
            system.gravityModifier = 0;
            particles[i].position = Vector3.Lerp(particles[i].position, playerPos, lerpTime * Time.unscaledDeltaTime);

            if (d < distanceToDestroy)
            {
                particles[i].remainingLifetime = 0;

            }           
        }
        lerpTime += Time.unscaledDeltaTime*25;

        // Apply the particle changes to the particle system
        system.SetParticles(particles, numParticlesAlive);
    }




   
}

/*public class ParticlesController : MonoBehaviour
{
    public float particleSpeed = 100f;
    public float timeToGoToPosition = 2f;

    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;

    private Vector3 playerPos;

    private float timer;
    private Car carInstance;
    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[system.main.maxParticles];
        carInstance = Car.instance;

        timer = 0;
    }

    private void Start()
    {
        system.Emit(30);
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > timeToGoToPosition)
        {
            ParticlesGoToPosition();
        }
    }

    private void ParticlesGoToPosition()
    {
        playerPos = carInstance.transform.position;


        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = system.GetParticles(particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            particles[i].velocity = Vector3.Normalize(playerPos - particles[i].position) * particleSpeed * Time.unscaledDeltaTime;


            if (Mathf.RoundToInt(particles[i].position.x) == Mathf.RoundToInt(playerPos.x) &&
                Mathf.RoundToInt(particles[i].position.y) == Mathf.RoundToInt(playerPos.y) &&
                Mathf.RoundToInt(particles[i].position.z) == Mathf.RoundToInt(playerPos.z))
            {
                particles[i].remainingLifetime = 0;
            }
        }

     
        // Apply the particle changes to the particle system
        system.SetParticles(particles, numParticlesAlive);
    }
}*/