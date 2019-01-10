using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDamage : MonoBehaviour
{
    [SerializeField]
    private int power = 5;

    [SerializeField]
    private int powerForStamina = 1;
    void OnParticleCollision(GameObject _obj)
    {
        //if (_obj.layer == 12 /*Uribou*/)
        //{
        //    EnemyFunctionRef enemyFunctionRef;
        //    enemyFunctionRef = _obj.transform.parent.GetComponent<EnemyFunctionRef>();
        //    enemyFunctionRef.GetEnemyController().StaminaDamageByLittleBubble(power);
        //}

        if (_obj.layer == 16 /*StageObject*/ || _obj.layer == 12 /*EnemyHit*/)
        {
            if (_obj.transform.root.GetComponent<ObjStatus>())
            {
                if (_obj.transform.root.GetComponent<ObjStatus>().Type == ObjType.Inoshishi)
                {
                    _obj.transform.root.GetComponent<BossStaminaCtr>().StaminaDamageByLittleBubble(powerForStamina);
                }
            }

            if (_obj.GetComponent<ObjController>())
            {
                _obj.GetComponent<ObjController>().DamageByBubble(power);
            }
            else
            {
                _obj.transform.root.GetComponent<ObjController>().DamageByBubble(power);
            }
        }
    }
}
