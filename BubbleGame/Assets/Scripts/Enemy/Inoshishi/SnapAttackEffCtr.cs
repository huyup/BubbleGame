using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class SnapAttackEffCtr : MonoBehaviour
{
    private void OnParticleCollision(GameObject _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (transform.GetComponent<ParticleSystem>())
                _other.GetComponent<PlayerController>().Damage();
        }
    }
}
