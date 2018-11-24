using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorCtr : PlayerController
{
    #region アニメーション用メソッド

    public void Test()
    {
        Debug.Log("Test");
    }
    public void SetMoveAnimation(Vector3 _preInputPlayerPos)
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - _preInputPlayerPos;

        if (direction.magnitude > 0)
        {
            Animator.SetBool("Moving", true);
        }
        else
        {
            Animator.SetBool("Moving", false);
        }
    }
    public void SetAttackAnimationOnButtonDown()
    {
        Animator.SetBool("Attacking", true);
    }
    public void SetAttackAnimationOnButtonStay()
    {
        Animator.speed = 0.5f;
    }
    public void SetAttackAnimationOnButtonUp()
    {
        Animator.speed = 1;
        Animator.SetBool("Attacking", false);

    }
    #endregion
}
