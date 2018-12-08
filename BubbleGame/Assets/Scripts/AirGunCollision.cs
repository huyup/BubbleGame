using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGunCollision : MonoBehaviour
{
    void OnParticleCollision(GameObject _obj)
    {
        Debug.Log(_obj);
        if (_obj.layer == 10 /*Bubble*/)
        {
            Vector3 particleDirection = this.transform.forward;
            _obj.GetComponent<BubbleController>().AddForceByPush(particleDirection*3);
        }
    }
}
