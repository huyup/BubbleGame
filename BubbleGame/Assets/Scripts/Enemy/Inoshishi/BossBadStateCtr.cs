﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
public class BossBadStateCtr : MonoBehaviour
{
    [SerializeField] private BehaviorTree dush;

    [SerializeField] private BehaviorTree think;

    [SerializeField]
    private GameObject dizzinessEff;

    [SerializeField]
    private GameObject poisonEff;

    [SerializeField] private GameObject poisonStatusEff;

    [SerializeField] private GameObject dizzinessStatusEff;

    [SerializeField]
    private float dizzinessLastTime = 10f;

    [SerializeField] private float poisonLastTime = 10f;

    [SerializeField] private int poisonDamagae = 2;
    private bool isInDizziness;

    private bool isInPoison;

    private float timeInterval = 1;

    private float timeElapsed;
    // Use this for initialization
    void Start()
    {
        isInDizziness = false;
        isInPoison = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInPoison)
        {
            timeElapsed += Time.fixedDeltaTime;

            if (timeElapsed >= timeInterval)
            {
                GetComponent<ObjController>().DamageByPoison(poisonDamagae);
                timeElapsed = 0;
            }
        }
    }
    public void BossPoison()
    {
        var particles = poisonEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particles)
        {
            particle.Play();
        }


        StartCoroutine(DelayPoison());
    }

    IEnumerator DelayPoison()
    {
        yield return new WaitForSeconds(1);

        var particlesStatus = poisonStatusEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particlesStatus)
        {
            particle.Play();
        }

        isInPoison = true;
        StartCoroutine(DelayResetStatusFromPoison());

    }
    IEnumerator DelayResetStatusFromPoison()
    {
        yield return new WaitForSeconds(poisonLastTime);
        var particles = poisonEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particles)
        {
            particle.Stop();
        }

        var particlesStatus = poisonStatusEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particlesStatus)
        {
            particle.Stop();
        }

        isInPoison = false;
    }
    public void BossDizziness()
    {
        var particles = dizzinessEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particles)
        {
            particle.Play();
        }
        dush.SetVariableValue("IsDizziness", true);
        think.SetVariableValue("IsDizziness", true);

        StartCoroutine(DelayDizziness());

    }

    IEnumerator DelayDizziness()
    {
        yield return new WaitForSeconds(1);
        var particlesStatus = dizzinessStatusEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particlesStatus)
        {
            particle.Play();
        }

        isInDizziness = true;
        StartCoroutine(DelayResetStatusFromDizziness());
    }
    IEnumerator DelayResetStatusFromDizziness()
    {
        yield return new WaitForSeconds(dizzinessLastTime);
        var particles = dizzinessEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particles)
        {
            particle.Stop();
        }
        var particlesStatus = dizzinessStatusEff.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particlesStatus)
        {
            particle.Stop();
        }
        dush.SetVariableValue("IsDizziness", false);
        think.SetVariableValue("IsDizziness", false);

        isInDizziness = false;
    }
}
