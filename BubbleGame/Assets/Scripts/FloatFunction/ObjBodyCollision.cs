using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                controller.OnReset();
            }
        }
        if (_other.gameObject.layer == 16 /*StageObj*/)
        {
            //オブジェクトにぶつかったときの処理
        }
        if (_other.gameObject.name == "DeadZone")
        {
            controller.Dead();
        }
        //Bossに当たったときの処理
        if ((_other.gameObject.layer == 12 /*EnemyHit*/|| _other.gameObject.layer == 15 /*EnemyAttack*/)
            && controller.ObjState == ObjState.MovingByAirGun)
        {
            collisionPos = transform.position;
            if (canHitBoss && _other.transform.root.GetComponent<ObjStatus>().Type == ObjType.Inoshishi)
            {
                controller.PlayCollisionEff(collisionPos);
                _other.transform.root.GetComponent<ObjController>().DamageByCollision(10);
                _other.transform.root.GetComponent<BossHateValueCtr>().IncreaseHateValueByCrash(50, controller.PlayerSelectionWhoPushed);
                StageManager.Instance.RemoveEnemyCount(this.gameObject.GetComponent<ObjStatus>().Type);
                Destroy(this.gameObject);
                canHitBoss = false;
            }
        }
    }


}
