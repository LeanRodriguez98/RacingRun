using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public float particleSpeed = 1f;
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
                Mathf.RoundToInt(particles[i].position.y) == Mathf.RoundToInt(playerPos.y))
            {
                particles[i].remainingLifetime = 0;
            }
        }

        // Apply the particle changes to the particle system
        system.SetParticles(particles, numParticlesAlive);
    }
}
