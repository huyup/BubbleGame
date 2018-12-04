﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDamage : MonoBehaviour
{
    [SerializeField]
    private int power = 5;

    void OnParticleCollision(GameObject _obj)
    {
        if (_obj.layer == 12 /*Enemy*/)
        {
            EnemyFunctionRef enemyFunctionRef;
            enemyFunctionRef = _obj.transform.parent.GetComponent<EnemyFunctionRef>();
            enemyFunctionRef.GetEnemyController().Damage(power);
        }

        if (_obj.layer == 17/*Environment*/)
            _obj.GetComponent<ObjController>().Damage(power);
    }
}
