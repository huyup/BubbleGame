using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これは敵の共通状態を管理するスクリプト
/// </summary>
public class EnemyStatus : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    private float walkSpeed = 6.0f;
    public float WalkSpeed
    {
        get { return walkSpeed; }
    }

    /// <summary>
    /// 回転速度
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 360.0f;
    public float RotateSpeed
    {
        get { return rotateSpeed; }
    }

    /// <summary>
    /// 最大Hp
    /// </summary>
    [SerializeField]
    private int maxHp = 100;
    public int MaxHp
    {
        get { return maxHp; }
    }

    /// <summary>
    /// 上昇速度の補正
    /// </summary>
    [SerializeField]
    private float upFactor = 1.1f;
    public float UpFactor
    {
        get { return upFactor; }
    }


}
