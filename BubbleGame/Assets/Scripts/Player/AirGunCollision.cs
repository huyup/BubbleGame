using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGunCollision : MonoBehaviour
{
    [SerializeField]
    private float airGunPower;
    void OnParticleCollision(GameObject _obj)
    {
        if (_obj.layer == 10 /*Bubble*/)
        {
            Vector3 particleDirection = this.transform.forward;
            _obj.GetComponent<BubbleController>().AddForceByPush(particleDirection* airGunPower);
            _obj.GetComponent<BubbleController>().SetBubbleState(BubbleState.BePressed);
        }
    }
}
