using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXEME:ここのパブリック変数を修正
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 死亡したかどうか
    /// </summary>
    [HideInInspector]
    public bool IsDied { get; set; }

    /// <summary>
    /// 浮上しているかどうか
    /// </summary>
    //[HideInInspector]
    public bool IsFloating = false;
    
    /// <summary>
    /// 攻撃目標
    /// </summary>
    protected Transform AttackTarget;

    /// <summary>
    /// 現在のHp
    /// </summary>
    protected int NowHp;

    [SerializeField]
    private EnemyFloatByDamage floatByDamage;

    [SerializeField]
    private EnemyFloatByContain floatByContain;

    protected void Update()
    {
        if (NowHp < 20)
        {
            FloatByDamage();
        }
    }

    public void ResetFloatFunction()
    {
        //TODO:ここに浮上状態のフラグをリセットさせる
        floatByContain.Reset();
        floatByDamage.Reset();
    }

    // 攻撃対象を設定する
    public void SetAttackTarget(Transform _target)
    {
        AttackTarget = _target;
    }

    public void FloatByContain(Transform _bubble)
    {
        floatByContain.FloatByContainOnInit(_bubble);
    }

    public void FloatByDamage()
    {
        floatByDamage.CreateBubbleByDamage();
    }

    public void Damage(int _power)
    {
        if (NowHp > 0)
            NowHp -= _power;

        GetComponent<EnemyMove>().SetSpeedByHp(NowHp);
    }
}
