using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXEME:ここのパブリック変数を修正
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 落下できるかどうか
    /// FIXME:回数にしたほうがいいか？
    /// </summary>
    //[HideInInspector]
    public bool canAddDownForce = true;

    /// <summary>
    /// 上昇できるかどうか
    /// FIXME:回数にしたほうがいいか？
    /// </summary>
    //[HideInInspector]
    public bool canAddUpForce = true;

    /// <summary>
    /// 泡の中にいるかどうか
    /// </summary>
    //[HideInInspector]
    public bool isInsideBubble { get; private set; }

    /// <summary>
    /// 上昇中かどうか
    /// </summary>
    //[HideInInspector]
    public bool isFloating = false;

    /// <summary>
    /// 速度を変えられるかどうか
    /// </summary>
    [HideInInspector]
    bool canChangeVelocity = false;

    /// <summary>
    /// 死亡したかどうか
    /// </summary>
    [HideInInspector]
    public bool IsDied { get;  set; }

    /// <summary>
    /// 攻撃目標
    /// </summary>
    protected Transform attackTarget;


    public void MoveToCenterPos(Transform bubble)
    {
        if (bubble == null)
        {
            GetComponent<Rigidbody>().velocity = Physics.gravity;
            ResetFloatFlag();
            return;
        }
        if (canAddUpForce && !isInsideBubble)
        {
            canChangeVelocity = true;
            isInsideBubble = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(SetFloatingFlag());
            GetComponent<CharacterController>().enabled = false;
        }
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
    public void SetRigibodyVelocityOnce(Vector3 _velocity)
    {
        if (canAddUpForce && !isInsideBubble)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(SetFloatingFlag());
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Rigidbody>().velocity = _velocity * 2f;
            canAddUpForce = false;
        }
    }
    IEnumerator SetFloatingFlag()
    {
        yield return new WaitForSeconds(1f);
        isFloating = true;
    }
    public void ResetFloatFlag()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        canAddDownForce = true;
        canAddUpForce = true;
        isFloating = false;
    }
    public void ResetComponent()
    {
        ResetFloatFlag();
        isInsideBubble = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (!GetComponent<CharacterController>().enabled)
            GetComponent<CharacterController>().enabled = true;
    }
    // 攻撃対象を設定する
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }
}
