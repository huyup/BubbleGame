using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjBodyCollision : MonoBehaviour
{
    [SerializeField]
    private ObjController controller;

    private bool canHitBoss = true;
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/&& controller.ObjState == ObjState.Falling)
        {
            controller.OnReset();
            if (GetComponent<ObjStatus>().Type != ObjType.Obj)
                controller.Dead();

        }

        if (_other.gameObject.layer == 12 /*EnemyHit*/
            && controller.ObjState == ObjState.MovingByAirGun)
        {
            if (canHitBoss)
            {
                _other.transform.root.GetComponent<ObjController>().SetObjState(ObjState.Dizziness);
                _other.transform.root.GetComponent<BossBehaviorCtr>().Dizziness();
                controller.Collision(_other.transform.position);
                canHitBoss = false;
            }
        }
    }
}
