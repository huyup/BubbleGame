using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これは敵の共用機能を管理するスクリプト
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 攻撃目標
    /// </summary>
    protected Transform AttackTarget;

    /// <summary>
    /// 現在のHp
    /// </summary>
    [SerializeField]
    protected int NowHp;
    
    [SerializeField]
    protected EnemyFunctionRef EnemyFunctionRef;

    /// <summary>
    /// 浮上しているかどうか
    /// </summary>
    //[HideInInspector]
    public bool IsFloating { get; private set; }

    /// <summary>
    /// 死亡したかどうか
    /// </summary>
    [HideInInspector]
    public bool IsDied { get; set; }

    public void Update()
    {
        CheckStatus();
    }
    public void SetIsDied(bool _isDied)
    {
        IsDied = _isDied;
    }
    public void SetIsFloating(bool _isFloating)
    {
        IsFloating = _isFloating;
    }
    private void CheckStatus()
    {
        if (NowHp < EnemyFunctionRef.GetEnemyParameter().FloatHp)
        {
            EnemyFunctionRef.GetEnemyFloatByDamage().StopEmitter();
            FloatByDamage();
        }
        else
        {
            EnemyFunctionRef.GetEnemyFloatByDamage().ChangeEmitterOnUpdate(EnemyFunctionRef.GetEnemyParameter().MaxHp, NowHp);
        }
    }

    public void OnReset()
    {
        EnemyFunctionRef.GetEnemyFloatByContain().Reset();
        EnemyFunctionRef.GetEnemyFloatByDamage().Reset();
        NowHp = EnemyFunctionRef.GetEnemyParameter().MaxHp;
        EnemyFunctionRef.GetEnemyMove().SetSpeedByHp(NowHp, EnemyFunctionRef.GetEnemyParameter().MaxHp);
        IsDied = false;
        IsFloating = false;
    }

    // 攻撃対象を設定する
    public void SetAttackTarget(Transform _target)
    {
        AttackTarget = _target;
    }

    public void FloatByContain(Transform _bubble)
    {
        EnemyFunctionRef.GetEnemyFloatByContain().FloatByContainOnInit(_bubble);
    }

    public void FloatByDamage()
    {
        EnemyFunctionRef.GetEnemyFloatByDamage().CreateBubbleByDamage();
    }

    public void Damage(int _power)
    {
        if (NowHp > 0)
            NowHp -= _power;

        EnemyFunctionRef.GetEnemyMove().SetSpeedByHp(NowHp, EnemyFunctionRef.GetEnemyParameter().MaxHp);
    }
    private void Died()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
