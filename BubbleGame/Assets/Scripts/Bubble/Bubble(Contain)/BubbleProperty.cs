using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProperty : MonoBehaviour
{
    /// <summary>
    /// 回転する速度
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 0.15f;
    public float RotateSpeed
    {
        get { return rotateSpeed; }
    }

    /// <summary>
    /// 存続時間
    /// </summary>
    [SerializeField]
    private float lastTime = 1f;
    public float LastTime
    {
        get { return lastTime; }
    }

    /// <summary>
    /// 空気砲に撃たれたときの存続時間
    /// </summary>
    [SerializeField]
    private float lastTimeByAirGun = 1f;
    public float LastTimeByAirGun
    {
        get { return lastTimeByAirGun; }
    }

    /// <summary>
    /// 最大サイズ
    /// </summary>
    [SerializeField]
    private float maxSize = 0.05f;
    public float MaxSize
    {
        get { return maxSize; }
    }

    /// <summary>
    ///敵を連れて上昇しているかどうか
    /// </summary>
    [SerializeField]
    private bool isForceFloating =false;
    public bool IsForceFloating
    {
        get { return isForceFloating; }
        set
        {
            isForceFloating = value;
        }
    }

    /// <summary>
    ///敵を連れて上昇しているかどうか
    /// </summary>
    [SerializeField]
    private bool isCreatedByDamage = false;
    public bool IsCreatedByDamage
    {
        get { return isCreatedByDamage; }
        set
        {
            isCreatedByDamage = value;
        }
    }
}
