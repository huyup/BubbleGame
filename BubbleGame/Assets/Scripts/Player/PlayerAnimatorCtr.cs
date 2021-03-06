﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorCtr : MonoBehaviour
{
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    #region アニメーション用メソッド
    public void SetMoveAnimation(Vector3 _preInputPlayerPos)
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - _preInputPlayerPos;

        if (direction.magnitude > 0.01f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    public void SetAttackAnimationOnButtonDown()
    {
        animator.SetBool("Attacking", true);
    }
    public void SetAttackAnimationOnButtonStay()
    {
        animator.speed = 0.8f;
    }
    public void SetAttackAnimationOnButtonUp()
    {
        animator.speed = 1;
        animator.SetBool("Attacking", false);

    }
    public void SetRevivalAnimation()
    {
        animator.SetBool("Revival", true);

    }

    public void SetOffFlagWhenRevival()
    {
        animator.SetBool("Revival", false);
        animator.SetBool("Dead", false);
    }
    public void SetDeadAnimation()
    {
        animator.SetBool("Dead", true);
    }
    #endregion
}
