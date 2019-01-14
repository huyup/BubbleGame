using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjBodyCollision : MonoBehaviour
{
    private ObjController controller;

    private bool canHitBoss = true;

    private void Start()
    {
        controller = GetComponent<ObjController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 /*Ground*/)
        {
            if (controller.ObjState == ObjState.Dead)
            {
                Debug.Log("Dead");
                controller.Dead();
            }
        }
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {
            Debug.Log("Dead");
            if (controller.ObjState == ObjState.Falling)
            {
                controller.OnReset();
            }
            else if (controller.ObjState == ObjState.Dead)
            {
                Debug.Log("Dead");
                controller.Dead();
            }
        }
        if (_other.gameObject.layer == 16 /*StageObj*/)
        {
            //オブジェクトにぶつかったときの処理
        }
        if (_other.gameObject.name == "DeadZone")
        {
            controller.DeadWhenOut();
        }
        //Bossに当たったときの処理
        if (_other.transform.root.CompareTag("Boss") &&
            Vector3.Distance(transform.position, _other.transform.root.position) > 4)
        {
            if (controller.ObjState == ObjState.MovingByAirGun && canHitBoss)
            {
                controller.PlayCollisionEff(transform.position);

                Damage(_other.gameObject);
                IncreaseHateValue(_other.gameObject);

                controller.ResetWhenCrash();

                canHitBoss = false;
            }
        }

    }

    private void IncreaseHateValue(GameObject _boss)
    {
        _boss.transform.root.GetComponent<BossHateValueCtr>()
            .IncreaseHateValueByCrash(50, controller.PlayerSelectionWhoPushed);
    }

    private void Damage(GameObject _Boss)
    {
        var objController = _Boss.transform.root.GetComponent<ObjController>();
        if (_Boss.transform.CompareTag("BossHead"))
        {
            if (transform.CompareTag("Uribou") || transform.CompareTag("Harinezumi"))
            {
                objController.DamageByCollision(6);
            }
            else if (transform.CompareTag("Stone"))
            {
                objController.DamageByCollision(10);
            }
            else if (transform.CompareTag("MushRoom"))
            {
                Debug.Log("Mushroom");
                objController.DamageByCollision(5);
            }
            else if (transform.CompareTag("FallDownTree"))
            {
                objController.DamageByCollision(15);
            }
        }
        else
        {
            if (transform.CompareTag("Uribou") || transform.CompareTag("Harinezumi"))
            {
                objController.DamageByCollision(10);
            }
            else if (transform.CompareTag("Stone"))
            {
                objController.DamageByCollision(15);
            }
            else if (transform.CompareTag("MushRoom"))
            {
                Debug.Log("Mushroom");
                objController.DamageByCollision(5);
            }
            else if (transform.CompareTag("FallDownTree"))
            {
                objController.DamageByCollision(15);
            }
        }
    }
}
