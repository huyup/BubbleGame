using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXEME:ここのパブリック変数を修正
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 泡の中にいるかどうか
    /// </summary>
    //[HideInInspector]
    public bool IsInsideBubble { get; private set; }

    /// <summary>
    /// 上昇中かどうか
    /// </summary>
    //[HideInInspector]
    public bool IsFloating = false;

    /// <summary>
    /// 落下中かどうか
    /// </summary>
    //[HideInInspector]
    public bool IsFalling = false;

    /// <summary>
    /// 速度を変えられるかどうか
    /// </summary>
    [HideInInspector]
    bool canChangeVelocity = false;

    /// <summary>
    /// 死亡したかどうか
    /// </summary>
    [HideInInspector]
    public bool IsDied { get; set; }

    /// <summary>
    /// 攻撃目標
    /// </summary>
    protected Transform AttackTarget;

    /// <summary>
    /// 現在のHp
    /// </summary>
    protected int NowHp;

    private Transform bubble;

    public void InitFloatFunction(Transform _bubble)
    {
        this.bubble = _bubble;
        IsFloating = true;
        canChangeVelocity = true;
        IsInsideBubble = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<CharacterController>().enabled = false;
    }

    protected void Update()
    {
        if (IsFloating)
        {
            if (bubble == null)
            {
                AddFallForce();
            }
            else
            {
                MoveToCenterPos();
            }
        }
    }

    private void MoveToCenterPos()
    {
        if (Vector3.Distance(transform.position, bubble.position) > 0.1f)
        {
            if (canChangeVelocity)
            {
                Vector3 direction = (bubble.position - transform.position).normalized;
                GetComponent<Rigidbody>().velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubble.position) < 0.1f)
        {
            canChangeVelocity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, bubble.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }

    private void AddFallForce()
    {
        GetComponent<Rigidbody>().velocity = Physics.gravity;
        IsFalling = true;
    }
    public void ResetFloatFunction()
    {
        IsFloating = false;
        IsInsideBubble = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (!GetComponent<CharacterController>().enabled)
            GetComponent<CharacterController>().enabled = true;
    }
    // 攻撃対象を設定する
    public void SetAttackTarget(Transform _target)
    {
        AttackTarget = _target;
    }

}
