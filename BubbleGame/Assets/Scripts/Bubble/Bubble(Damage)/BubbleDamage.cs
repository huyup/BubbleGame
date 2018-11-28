using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDamage : MonoBehaviour
{
    [SerializeField]
    private int power = 5;

    void OnParticleCollision(GameObject _obj)
    {
        _obj.GetComponent<EnemyController>().Damage(power);
    }
}
