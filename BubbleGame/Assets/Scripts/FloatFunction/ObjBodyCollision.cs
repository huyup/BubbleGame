using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjBodyCollision : MonoBehaviour
{
    private ObjController selfController;

    private bool canHitBoss = true;

    private void Start()
    {
        selfController = GetComponent<ObjController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 /*Ground*/)
        {
            selfController.OnReset();
            canHitBoss = true;
        }
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {
            if (selfController.ObjState == ObjState.Falling)
            {
                selfController.OnReset();
                canHitBoss = true;
            }
        }
        if (_other.gameObject.layer == 16 /*StageObj*/)
        {
            //オブジェクトにぶつかったときの処理
        }
        if (_other.gameObject.name == "DeadZone")
        {
            selfController.DeadWhenOut();
        }
        //Bossに当たったときの処理
        if (_other.transform.root.CompareTag("Boss") &&
            Vector3.Distance(transform.position, _other.transform.root.position) > 4)
        {
            Debug.Log("Crash");
            if (selfController.ObjState == ObjState.MovingByAirGun && canHitBoss)
            {
                Debug.Log("Crash2");
                selfController.PlayCollisionEff(transform.position);
                Damage(_other.gameObject, _other.transform.position);
                IncreaseHateValue(_other.gameObject);

                canHitBoss = false;
            }
        }

    }

    private void IncreaseHateValue(GameObject _boss)
    {
        _boss.transform.root.GetComponent<BossHateValueCtr>()
            .IncreaseHateValueByCrash(50, selfController.PlayerSelectionWhoPushed);
    }

    private void Damage(GameObject _Boss, Vector3 _otherPosition)
    {
        var bossController = _Boss.transform.root.GetComponent<ObjController>();
        if (_Boss.transform.CompareTag("BossHead"))
        {
            if (transform.CompareTag("Uribou") || transform.CompareTag("Harinezumi"))
            {
                bossController.DamageByCollision(6);
                selfController.EnemyCrash(_otherPosition);
            }
            else if (transform.CompareTag("Stone"))
            {
                selfController.StoneCrash();
                bossController.DamageByCollision(10);
            }
            else if (transform.CompareTag("MushRoom"))
            {
                bossController.BossDizziness();
                bossController.DamageByCollision(5);
                selfController.MushroomCrash(_otherPosition);
            }
        }
        else
        {
            if (transform.CompareTag("Uribou") || transform.CompareTag("Harinezumi"))
            {
                bossController.DamageByCollision(10);
                selfController.EnemyCrash(_otherPosition);
            }
            else if (transform.CompareTag("Stone"))
            {
                selfController.StoneCrash();
                bossController.DamageByCollision(15);
            }
            else if (transform.CompareTag("MushRoom"))
            {
                bossController.DamageByCollision(5);
                selfController.MushroomCrash(_otherPosition);
            }
        }
    }
}
