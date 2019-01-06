using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGunCtr : MonoBehaviour
{
    [SerializeField]
    private float airGunPower;

    [SerializeField]
    private float maxStartSize = 5;

    [SerializeField]
    private float forceOverTimeInZ = 100;

    [SerializeField]
    private float startSpeed = 10;

    [SerializeField]
    private float sphereSize = 1f;

    private ParticleSystem airGunParticle;

    [SerializeField]
    private ParticleSystem shadowParticle;

    [SerializeField]
    private ParticleSystem deathParticle;
    private void Start()
    {
        airGunParticle = GetComponent<ParticleSystem>();
    }


    public void SetAirGunParticle(float _Power, float _MaxPower)
    {
        //MaxPower 
        //Max StartSize 5
        //Force Overtime  z 100
        //StartSpeed 10

        //200->1 100->0.5 0->0
        //200-100=100 
        float newRate = (_Power * 100 / _MaxPower) * 0.01f;

        float newStartSize = maxStartSize * newRate;
        float newStartSpeed = startSpeed * newRate;
        float newForceOverTimeInZ = forceOverTimeInZ * newRate;
        float newDeathShapeSize = sphereSize * newRate;

        var main = airGunParticle.main;
        main.startSpeed = newStartSpeed;
        main.startSize = newStartSize;

        var force = airGunParticle.forceOverLifetime;
        force.z = newForceOverTimeInZ;


        var main2 = shadowParticle.main;
        main2.startSpeed = newStartSpeed;
        main2.startSize = newStartSize * 0.5f;

        var force2 = shadowParticle.forceOverLifetime;
        force2.z = newForceOverTimeInZ;


        var main3 = deathParticle.main;
        main3.startSize = newStartSize * 0.1f;
        var shape = deathParticle.shape;
        shape.radius = newDeathShapeSize;


        Debug.Log("Rate" + newRate);

    }
    public void SetAirGunPower(float _Power)
    {
        airGunPower = _Power;
    }
    void OnParticleCollision(GameObject _obj)
    {
        if (_obj.layer == 17 /*BubbleAirCollider*/)
        {
            Vector3 particleDirection = this.transform.forward;
            _obj.transform.parent.GetComponent<BubbleController>().AddForceByPush(particleDirection * airGunPower);
            _obj.transform.parent.GetComponent<BubbleController>().SetBubbleState(BubbleState.BePressed);
        }
    }
}
