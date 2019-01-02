using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerSelection
{
    Player1 = 1,
    Player2,
};
public class PlayerStatus : MonoBehaviour
{


    public PlayerSelection playerNum = PlayerSelection.Player1;

    /// <summary>
    /// プレイヤーの番号
    /// </summary>
    //FIXME:ここにNumのsetの制限を追加する
    public int Num
    {
        get { return (int)playerNum; }
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
            if (runSpeed > 0 && runSpeed != 0)
                runSpeed = value;
        }
    }

    /// <summary>
    /// 移動時の速度
    /// </summary>
    [SerializeField]
    private float backRunSpeed = 3;
    public float BackRunSpeed
    {
        get { return backRunSpeed; }
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
    float bubbleFowardPower = 15f;
    public float BubbleFowardPower
    {
        get { return bubbleFowardPower; }
        private set
        {
            if (bubbleFowardPower > 0 && bubbleFowardPower != 0)
                bubbleFowardPower = value;
        }
    }


    /// <summary>
    /// 泡を上に押す力
    /// </summary>
    [SerializeField]
    float bubbleUpPower = 15f;
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
    float spaceKeySpeed = 0.15f;
    public float SpaceKeySpeed
    {
        get { return spaceKeySpeed; }
        private set
        {
            if (spaceKeySpeed > 0 && spaceKeySpeed != 0)
                spaceKeySpeed = value;
        }
    }

    /// <summary>
    /// 反動の強さ
    /// </summary>
    [SerializeField]
    float pullBackSpeed = 3f;
    public float PullBackSpeed
    {
        get { return pullBackSpeed; }
    }

    /// <summary>
    /// 反動の時間
    /// </summary>
    [SerializeField]
    float pullBackTime = 0.5f;
    public float PullBackTime
    {
        get { return pullBackTime; }
    }

    /// <summary>
    /// 空気砲を使える時間
    /// </summary>
    [SerializeField]
    float airGunLastTime = 10;
    public float AirGunLastTime
    {
        get { return airGunLastTime; }
    }
    #endregion

    #region 被ダメージ用
    /// <summary>
    /// 点滅間隔
    /// </summary>
    [SerializeField]
    float revivalAnimationTime = 1.3f;
    public float RevivalAnimationTime
    {
        get { return revivalAnimationTime; }
    }


    /// <summary>
    /// 点滅間隔
    /// </summary>
    [SerializeField]
    float invincibleInterval = 0.1f;
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
    float invincibleTotalTime = 20f;
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
    int maxHp = 3;
    public int MaxHp
    {
        get { return maxHp; }
    }

    [SerializeField]
    public int nowHp;
    
    #endregion

    public void Start()
    {
        nowHp = MaxHp;
    }
}
