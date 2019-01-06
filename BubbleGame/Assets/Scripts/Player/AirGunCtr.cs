using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGunCtr : MonoBehaviour
{
    [SerializeField]
    private float airGunPower;


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
