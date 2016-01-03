using UnityEngine;

[ExecuteInEditMode]
public class ParticleDeactivator : MonoBehaviour
{
    ParticleSystem.Particle[] outUnused = new ParticleSystem.Particle[1];

    ParticleSystem sys;
    ParticleSystemRenderer rend;

    void Awake()
    {
        rend = GetComponent<ParticleSystemRenderer>();
        sys = GetComponent<ParticleSystem>();
        rend.enabled = false;
    }

    void LateUpdate()
    {
        rend.enabled = sys.GetParticles(outUnused) > 0;
    }
}