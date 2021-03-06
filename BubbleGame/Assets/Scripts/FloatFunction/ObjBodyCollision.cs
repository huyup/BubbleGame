﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjBodyCollision : MonoBehaviour
{
    private ObjController selfController;

    private bool canHitBoss = true;

    [SerializeField] private BossBadStateCtr bossBadStateCtr;

    [SerializeField] private int stoneDamageInHead = 15;

    [SerializeField] private int stoneDamageInBody = 10;
    private void Start()
    {
        selfController = GetComponent<ObjController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 /*Ground*/)
        {
            if (selfController.ObjState != ObjState.Floating)
            {
                Debug.Log("Reset");
                selfController.OnReset();
                canHitBoss = true;
            }
        }
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {
            if (selfController.ObjState == ObjState.Falling)
            {
                Debug.Log("Reset");
                selfController.OnReset();
                canHitBoss = true;
            }
        }
        if (_other.gameObject.layer == 16 /*StageObj*/)
        {
            ////オブジェクトにぶつかったときの処理
            //_other.GetComponent<ObjController>().ObjCrash();
        }
        if (_other.gameObject.name == "DeadZone")
        {
            selfController.DeadWhenOut();
        }
        //Bossに当たったときの処理
        if (_other.transform.root.CompareTag("Boss") &&
            Vector3.Distance(transform.position, _other.transform.root.position) > 4)
        {
            if (selfController.ObjState == ObjState.MovingByAirGun && canHitBoss)
            {

                Damage(_other.gameObject, _other.transform.position);
                IncreaseHateValue(_other.gameObject);

                canHitBoss = false;
            }
        }

    }

    private void IncreaseHateValue(GameObject _boss)
    {
        _boss.transform.root.GetComponent<BossHateValueCtr>()
            .IncreaseHateValueByCrash(10, selfController.PlayerSelectionWhoPushed);
    }

    private void Damage(GameObject _Boss, Vector3 _otherPosition)
    {
        var bossController = _Boss.transform.root.GetComponent<ObjController>();
        if (_Boss.transform.CompareTag("BossHead"))
        {
            if (transform.CompareTag("Uribou") || transform.CompareTag("Harinezumi"))
            {
                selfController.HitBossEff(transform.position);
                bossController.DamageByCollision(6);
                selfController.EnemyCrash(_otherPosition);
            }
            else if (transform.CompareTag("Stone"))
            {
                selfController.HitBossEff(transform.position);
                selfController.StoneCrash();
                bossController.DamageByCollision(stoneDamageInHead);
            }
            else if (transform.CompareTag("MushRoom"))
            {
                if (selfController.MushroomType == MushroomType.PoisonMushroom)
                {
                    _Boss.transform.root.GetComponent<BossBadStateCtr>().BossPoison();
                }
                else
                {
                    _Boss.transform.root.GetComponent<BossBadStateCtr>().BossDizziness();
                }

                bossController.DamageByCollision(5);
                selfController.MushroomCrashWithHead(_otherPosition);
            }
        }
        else
        {
            if (transform.CompareTag("Uribou") || transform.CompareTag("Harinezumi"))
            {
                selfController.HitBossEff(transform.position);
                bossController.DamageByCollision(10);
                selfController.EnemyCrash(_otherPosition);
            }
            else if (transform.CompareTag("Stone"))
            {
                selfController.HitBossEff(transform.position);
                selfController.StoneCrash();
                bossController.DamageByCollision(stoneDamageInBody);
            }
            else if (transform.CompareTag("MushRoom"))
            {
                selfController.HitBossEff(transform.position);
                bossController.DamageByCollision(5);
                selfController.EnemyCrash(_otherPosition);
            }
        }
    }
}
