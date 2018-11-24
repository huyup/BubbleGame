using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum PlayerSelection
    {
        Player1 = 1,
        Player2,
    };

    public PlayerSelection PlayerNum = PlayerSelection.Player1;

    /// <summary>
    /// プレイヤーの番号
    /// </summary>
    public int Num
    {
        get { return (int)PlayerNum; }
    }


    #region 移動・ジャンプ用
    /// <summary>
    /// 移動時の速度
    /// </summary>
    [SerializeField]
    private float runSpeed = 5;
    public float RunSpeed
    {
        get { return runSpeed; }
        private set
        {
            runSpeed = value;
        }
    }

    /// <summary>
    /// ジャンプパワー
    /// </summary>
    [SerializeField]
    private float jumpPower = 1000;
    public float JumpPower
    {
        get { return jumpPower; }
        private set
        {
            if (jumpPower > 0 && jumpPower != 0)
                jumpPower = value;
        }
    }

    /// <summary>
    /// 重力補正
    /// </summary>
    [SerializeField]
    private float factorToCalGravity = 0.05f;
    public float FactorToCalGravity
    {
        get { return factorToCalGravity; }
        private set
        {
            if (factorToCalGravity > 0 && factorToCalGravity != 0)
                factorToCalGravity = value;
        }
    }

    #endregion
    #region 攻撃用
    /// <summary>
    /// 泡を前に押す力
    /// </summary>
    [SerializeField]
    private float bubbleForwardPower = 15f;
    public float BubbleForwardPower
    {
        get { return bubbleForwardPower; }
        private set
        {
            if (bubbleForwardPower > 0 && bubbleForwardPower != 0)
                bubbleForwardPower = value;
        }
    }


    /// <summary>
    /// 泡を上に押す力
    /// </summary>
    [SerializeField]
    private float bubbleUpPower = 15f;
    public float BubbleUpPower
    {
        get { return bubbleUpPower; }
        private set
        {
            if (bubbleUpPower > 0 && bubbleUpPower != 0)
                bubbleUpPower = value;
        }
    }

    /// <summary>
    /// 泡の拡大スピード
    /// </summary>
    [SerializeField]
    private float spaceKeySpeed = 0.15f;
    public float SpaceKeySpeed
    {
        get { return spaceKeySpeed; }
        private set
        {
            if (spaceKeySpeed > 0 && spaceKeySpeed != 0)
                spaceKeySpeed = value;
        }
    }
    #endregion

    #region 被ダメージ用
    /// <summary>
    /// 点滅間隔
    /// </summary>
    [SerializeField]
    private float invincibleInterval = 0.1f;
    public float InvincibleInterval
    {
        get { return invincibleInterval; }
        private set
        {
            invincibleInterval = value;
        }
    }

    /// <summary>
    /// 無敵時間
    /// </summary>
    [SerializeField]
    private float invincibleTotalTime = 20f;
    public float InvincibleTotalTime
    {
        get { return invincibleTotalTime; }
        private set
        {
            invincibleTotalTime = value;
        }
    }

    /// <summary>
    /// 最大hp
    /// </summary>
    [SerializeField]
    private int maxHp = 3;
    public int MaxHp
    {
        get { return MaxHp; }
    }


    #endregion

}
