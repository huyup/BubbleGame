using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackActivator : MonoBehaviour
{
    GameObject attackObj;
    Collider attackCollider;
    // Use this for initialization
    void Start()
    {
        attackObj = transform.GetComponentInChildren<AttackArea>().gameObject;
        attackCollider = attackObj.GetComponent<SphereCollider>();
        attackCollider.enabled = false;
    }

    // アニメーションイベントのStartAttackHitを受け取ってコライダを有効にする
    void StartAttackHit()
    {
        attackCollider.enabled = true;
    }

    // アニメーションイベントのEndAttackHitを受け取ってコライダを無効にする
    void EndAttackHit()
    {
        attackCollider.enabled = false;
    }
}
