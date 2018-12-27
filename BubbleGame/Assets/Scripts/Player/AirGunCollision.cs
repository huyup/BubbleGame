using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGunCollision : MonoBehaviour
{
    [SerializeField]
    private float airGunPower;
    void OnParticleCollision(GameObject _obj)
    {
        if (_obj.layer == 17 /*BubbleAirCollider*/)
        {
            Vector3 particleDirection = this.transform.forward;
            _obj.transform.parent.GetComponent<BubbleController>().AddForceByPush(particleDirection* airGunPower);
            _obj.transform.parent.GetComponent<BubbleController>().SetBubbleState(BubbleState.BePressed);
        }
    }
}
