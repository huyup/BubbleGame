﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これは敵の共通パラメータを管理するスクリプト
/// </summary>
public class EnemyCommonParameter : MonoBehaviour
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

    
    #region 浮上用
    /// <summary>
    /// 浮上するときのHp
    /// </summary>
    [SerializeField]
    private int floatHp = 20;
    public int FloatHp
    {
        get { return floatHp; }
    }




    #endregion


}
