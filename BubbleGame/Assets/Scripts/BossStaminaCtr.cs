using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class BossStaminaCtr : MonoBehaviour
{
    [SerializeField]
    private float maxStamina = 100;

    private float stamina;

    [SerializeField]
    private float staminaRecoverySpeed = 1;

    [SerializeField]
    private GameObject damageEff;

    [SerializeField]
    private BehaviorTree dash;

    [SerializeField]
    private float dashSpeedWhenOutOfStamina = 3;

    private bool canStartRecovering = false;

    private bool isRecovering = false;

    private bool isOutOfStamina = false;

    private float defaultDashSpeed;
    // Use this for initialization
    void Start()
    {
        defaultDashSpeed = (float)dash.GetVariable("DashSpeed").GetValue();
        stamina = maxStamina;
    }
    private void Update()
    {
        if (isOutOfStamina)
        {
            OutOfStamina();
        }

        if (canStartRecovering)
        {
            StaminaRecovery();
        }
    }
    private void OutOfStamina()
    {
        ParticleSystem.EmissionModule emissionModule = damageEff.GetComponent<ParticleSystem>().emission;
        emissionModule.rateOverTime = 20;
        damageEff.GetComponent<ParticleSystem>().Play();

        dash.SetVariableValue("DashSpeed", dashSpeedWhenOutOfStamina);

        canStartRecovering = true;
        isOutOfStamina = false;
    }
    private void StaminaRecovery()
    {
        if (stamina >= maxStamina)
        {
            Debug.Log("Over");
            damageEff.GetComponent<ParticleSystem>().Clear();
            damageEff.GetComponent<ParticleSystem>().Stop();
            ParticleSystem.EmissionModule emissionModule = damageEff.GetComponent<ParticleSystem>().emission;
            emissionModule.rateOverTime = 0;

            dash.SetVariableValue("DashSpeed", defaultDashSpeed);
            stamina = maxStamina;
            canStartRecovering = false;
            isRecovering = false;
        }
        isRecovering = true;
        stamina += staminaRecoverySpeed * Time.fixedDeltaTime * 60;
    }

    public void StaminaDamageByLittleBubble(int _power)
    {
        if (isRecovering)
            return;

        if (stamina > 0)
            stamina -= _power;
        else
        {
            stamina = 0;
            isOutOfStamina = true;
        }
    }

    public void StaminaDamageByBigBubble(float _sizeMagnitude)
    {
        if (isRecovering)
            return;
        if (stamina > 0)
            stamina -= _sizeMagnitude;
        else
        {
            stamina = 0;
            isOutOfStamina = true;
        }
    }
    public int GetNowStamina()
    {
        return (int)stamina;
    }
}
