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

    public struct particleBezier
    {
        public Vector3 p0;
        public Vector3 p1;
        public Vector3 p2;
        public Vector3 p3;
        public float t;
    }

    private List<particleBezier> particleBeziers;

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[system.main.maxParticles];
        particleBeziers = new List<particleBezier>();
        carInstance = Car.instance; 

        timer = 0;
    }

    private void Start()
    {
        Invoke("SetVezierPoints", timeToGoToPosition);
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
        for (int i = 0; i < particles.Length; i++)
        {
            //particles[i].velocity = Vector3.Normalize(playerPos - particles[i].position) * particleSpeed * Time.unscaledDeltaTime;

            particles[i].position = ((1 - particleBeziers[i].t) * (1 - particleBeziers[i].t) * (1 - particleBeziers[i].t)) * particleBeziers[i].p0 + 3 * ((1 - particleBeziers[i].t) * (1 - particleBeziers[i].t)) * particleBeziers[i].t * particleBeziers[i].p1 + 3 * (1 - particleBeziers[i].t) * (particleBeziers[i].t * particleBeziers[i].t) * particleBeziers[i].p2 + (particleBeziers[i].t * particleBeziers[i].t * particleBeziers[i].t) * playerPos;// particleBeziers[i].p3;
            particleBezier pb = new particleBezier();
            pb = particleBeziers[i];
            pb.t += Time.unscaledDeltaTime * 0.05f;
            particleBeziers[i] = pb;
            if (Mathf.RoundToInt(particles[i].position.x) == Mathf.RoundToInt(playerPos.x) &&
                Mathf.RoundToInt(particles[i].position.y) == Mathf.RoundToInt(playerPos.y) &&
                Mathf.RoundToInt(particles[i].position.z) == Mathf.RoundToInt(playerPos.z))
            {
                particles[i].remainingLifetime = 0;
                particleBeziers.RemoveAt(i);
            }
        }

        // Apply the particle changes to the particle system
       // system.SetParticles(particles, numParticlesAlive);
    }

    private void SetVezierPoints()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particleBezier pb = new particleBezier();
            pb.p0 = particles[i].position;
            pb.p3 = carInstance.transform.position;
            pb.p1 = ((pb.p3 - pb.p0) * 0.33f) + new Vector3(0, Random.Range(1,5), 0);
            pb.p2 = ((pb.p3 - pb.p0) * 0.66f) + new Vector3(0, Random.Range(1,5), 0);
            pb.t = 0;
            particleBeziers.Add(pb);

        }
    }

    private void OnDisable()
    {
        system.time = 0;
    }

    private void OnEnable()
    {
        system.Play();
    }
}
