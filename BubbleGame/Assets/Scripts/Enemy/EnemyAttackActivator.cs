using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackActivator : MonoBehaviour
{
    [SerializeField]
    private Collider attackCollider;

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
