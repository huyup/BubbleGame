using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDamageEff : MonoBehaviour
{
    private ParticleSystem bubbleDamageParticleSystem;

    [SerializeField]
    private float factorToCalEmission = 3;

    void Start()
    {
        bubbleDamageParticleSystem = GetComponent<ParticleSystem>();
    }

    public void ChangeEmitterOnUpdate(int _maxHp, int _nowHp)
    {
        ParticleSystem.EmissionModule emissionModule = bubbleDamageParticleSystem.emission;
        emissionModule.rateOverTime = (((_maxHp / 10) - (_nowHp / 10))) * factorToCalEmission;
    }

    public void StopEmitter()
    {
        if (bubbleDamageParticleSystem == null)
            return;
        bubbleDamageParticleSystem.Clear();
        bubbleDamageParticleSystem.Stop();
    }

    public void ResetEmitter()
    {

        if (!bubbleDamageParticleSystem.isPlaying)
        {
            ParticleSystem.EmissionModule emissionModule = bubbleDamageParticleSystem.emission;

            emissionModule.rateOverTime = 0;
            bubbleDamageParticleSystem.Clear();
            bubbleDamageParticleSystem.Play();
        }
    }
}
