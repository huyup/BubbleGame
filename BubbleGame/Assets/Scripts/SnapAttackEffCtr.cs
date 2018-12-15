using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class SnapAttackEffCtr : MonoBehaviour
{
    private ParticleSystem[] particleSystems;

    [SerializeField]
    private float waveSpeed = 0.2f;

    [SerializeField]
    private BehaviorTree waveAttack;

    private float initStartSize;

    private float startSize;
    private bool canStartWaveAttack = true;
    // Use this for initialization
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        var ma = particleSystems[0].main;
        initStartSize = ma.startSize.constant;
        startSize = initStartSize;
        canStartWaveAttack = (bool)waveAttack.GetVariable("CanStartWaveAttack").GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        canStartWaveAttack = (bool)waveAttack.GetVariable("CanStartWaveAttack").GetValue();
        if (canStartWaveAttack)
        {
            foreach (var particle in particleSystems)
            {
                startSize += Time.fixedDeltaTime * 60 * waveSpeed;
                var ma = particle.main;
                ma.startSize = new ParticleSystem.MinMaxCurve(startSize, startSize);

            }
        }
        else
        {
            foreach (var particle in particleSystems)
            {
                var ma = particle.main;
                ma.startSize = new ParticleSystem.MinMaxCurve(initStartSize, initStartSize);
                startSize = initStartSize;
            }
        }
    }
}
