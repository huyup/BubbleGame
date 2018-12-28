using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ObjBodyCollision : MonoBehaviour
{
    private ObjController controller;

    private bool canHitBoss = true;

    private void Start()
    {
        controller = GetComponent<ObjController>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {
            if (controller.ObjState == ObjState.Falling)
            controller.OnReset();
        }
        if (_other.gameObject.layer == 16 /*StageObj*/)
        {
            if ((_other.GetComponent<ObjController>().ObjState == ObjState.Falling ||
                 _other.GetComponent<ObjController>().ObjState == ObjState.MovingByAirGun))
            {
                if (GetComponent<ObjStatus>().Type != ObjType.Obj)
                    controller.Dead();
            }
        }
        if (_other.gameObject.layer == 12 /*EnemyHit*/
            && controller.ObjState == ObjState.MovingByAirGun)
        {
            if (canHitBoss)
            {
                _other.transform.root.GetComponent<ObjController>().SetObjState(ObjState.Dizziness);
                _other.transform.root.GetComponent<BehaviorsCtr>().Dizziness();
                controller.Collision(_other.transform.position);
                canHitBoss = false;
            }
        }
    }
}
