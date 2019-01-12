using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDamage : MonoBehaviour
{
    [SerializeField]
    private int power = 5;

    [SerializeField]
    private int powerForStamina = 1;

    [SerializeField]
    private float powerForHateValue = 3;
    void OnParticleCollision(GameObject _obj)
    {
        if (_obj.layer == 16 /*StageObject*/ || _obj.layer == 12 /*EnemyHit*/)
        {

            if (_obj.GetComponent<ObjController>())
            {
                _obj.GetComponent<ObjController>().DamageByBubble(power);
            }
            else
            {
                _obj.transform.root.GetComponent<ObjController>().DamageByBubble(power);
            }
        }

        if (_obj.transform.root.CompareTag("Boss"))
        {
            _obj.transform.root.GetComponent<BossStaminaCtr>().StaminaDamageByLittleBubble(powerForStamina);

            var playerSelection = transform.GetComponentInParent<PlayerStatus>().PlayerSelection;

            _obj.transform.root.GetComponent<BossHateValueCtr>()
                .IncreaseHateValueByLittleBubble(powerForHateValue, playerSelection);
        }

    }
}
