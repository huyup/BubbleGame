using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerController : MonoBehaviour
{
    protected Rigidbody Rb;
    protected Animator Animator;

    [HideInInspector]
    protected PlayerStatus Status;

    [HideInInspector] private PlayerJumpCtr jumpCtr;
    [HideInInspector] private PlayerMoveCtr moveCtr;
    [HideInInspector] private PlayerAttackCtr attackCtr;
    [HideInInspector] private PlayerDamageCtr damageCtr;
    [HideInInspector] private PlayerRotateCtr rotateCtr;
    [HideInInspector] private PlayerAnimatorCtr animatorCtr;

    private List<PlayerController> ctrs;

    public virtual void OnInitialize()
    {
        jumpCtr = GetComponent<PlayerJumpCtr>();
        moveCtr = GetComponent<PlayerMoveCtr>();
        attackCtr = GetComponent<PlayerAttackCtr>();
        rotateCtr = GetComponent<PlayerRotateCtr>();
        damageCtr = GetComponent<PlayerDamageCtr>();
        animatorCtr = GetComponent<PlayerAnimatorCtr>();

        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        Debug.Log("OnInitialize");
    }

    public virtual void OnStart()
    {
    }

    public virtual void OnUpdate()
    {
        jumpCtr.OnUpdate();
        moveCtr.OnUpdate();
        attackCtr.OnUpdate();
        rotateCtr.OnUpdate();
        damageCtr.OnUpdate();
        animatorCtr.OnUpdate();
    }

    public PlayerAttackCtr GetAttackCtr()
    {
        return attackCtr;
    }
    public PlayerAnimatorCtr GetAnimatorCtr()
    {
        return animatorCtr;
    }
    public PlayerDamageCtr GetDamageCtr()
    {
        return damageCtr;
    }
    public PlayerJumpCtr GetJumpCtr()
    {
        return jumpCtr;
    }
    public PlayerRotateCtr GetRotateCtr()
    {
        return rotateCtr;
    }
    public PlayerMoveCtr GetMoveCtr()
    {
        return moveCtr;
    }
}
