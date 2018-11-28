using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO:狼専用のアニメーター
/// </summary>
public class WargAnimator : EnemyAnimator
{
    WargController controller;
    WargsStatus status;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<WargController>();
        status = transform.GetComponent<WargsStatus>();
    }


    public override void SetMoveAnimatorParameter()
    {
        if (controller.GetAttackedFlag() && !controller.attacking)
        {
            controller.TurnOffAttackedFlag();
        }

        if (controller.IsFloating)
        {     
            animator.SetFloat("Speed",0);
            animator.SetBool("Attacking", false);
        }
        else
        {        //移動パラメータ
            Vector3 delta_position = transform.position - new Vector3(0, transform.position.y, 0) - prePositionXZ;

            animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
            animator.SetBool("Attacking", (!controller.GetAttackedFlag() && controller.attacking));
        }
        prePositionXZ = transform.position - new Vector3(0, transform.position.y, 0);
    }
    void EndAttack()
    {
        controller.TurnOffAttackedFlag();
    }

    public void StopAttackAnimation()
    {
        animator.SetBool("Attacking", false);
    }
}
