using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ObjBodyCollision : MonoBehaviour
{
    private ObjController controller;

    private bool canHitBoss = true;


    private Vector3 collisionPos;
    private void Start()
    {
        controller = GetComponent<ObjController>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {

            if (controller.ObjState == ObjState.Falling)
            {
                Debug.Log("ObjState"+ controller.ObjState);
                controller.OnReset();
            }
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
        if ((_other.gameObject.layer == 12 /*EnemyHit*/|| _other.gameObject.layer == 15 /*EnemyAttack*/)
            && controller.ObjState == ObjState.MovingByAirGun)
        {
            collisionPos = transform.position;
            if (canHitBoss && _other.transform.root.GetComponent<ObjStatus>().Type == ObjType.Inoshishi)
            {
                controller.PlayCollisionEff(collisionPos);
                _other.transform.root.GetComponent<ObjController>().DamageByCollision(10);
                Destroy(this.gameObject);
                canHitBoss = false;
            }
            else
            {
                if (GetComponent<ObjStatus>().Type != ObjType.Obj)
                {
                    controller.Dead();
                    _other.transform.root.GetComponentInChildren<ObjController>().Dead();
                }
            }
        }
    }

    
}
