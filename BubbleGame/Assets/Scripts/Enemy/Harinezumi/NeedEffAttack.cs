using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class NeedEffAttack : MonoBehaviour
{
    [SerializeField]
    private BehaviorTree attack;

    private GameObject nowTarget;

    private ParticleSystem.MainModule main;
    private void Start()
    {
        main = GetComponent<ParticleSystem>().main;
    }

    private void OnParticleCollision(GameObject _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerController>().Damage();
        }
    }

    private void Update()
    {
        if ((GameObject)attack.GetVariable("Target").GetValue())
        {
            nowTarget = (GameObject)attack.GetVariable("Target").GetValue();

            Vector3 targetPosXZ = nowTarget.transform.position - new Vector3(0, nowTarget.transform.position.y, 0);

            float distance = Vector3.Distance(targetPosXZ, transform.parent.position);

            if (distance > 13 && distance < 16)
            {
                main.startSpeed = 10;
                //speed 10
            }
            if (distance > 10 && distance < 13)
            {
                main.startSpeed = 8;
            }
            if (distance > 10 && distance < 7)
            {
                main.startSpeed = 6;
            }
            if (distance < 7)
            {
                main.startSpeed = 5;
            }

        }
    }
}
