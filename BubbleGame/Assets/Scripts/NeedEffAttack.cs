using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedEffAttack : MonoBehaviour {

    private void OnParticleCollision(GameObject _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerController>().Damage();
        }
    }
}
