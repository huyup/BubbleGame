using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ステートの種類.
public enum WargState
{
    Searching,    // 探索
    Chasing,    // 追跡
    Attacking,  // 攻撃
    Died,       // 死亡
};
public class WargController : EnemyController
{
    WargSearch searchController;
    WargsCommonParameter commonParameter;
    WargAnimator animator;
    // 残り待機時間
    float waitTime;

    // 初期位置を保存しておく変数
    Vector3 initPosition;

    // 複数のアイテムを入れれるように配列にしましょう。
    public GameObject[] dropItemPrefab;

    //状態.
    public bool attacking = false;
    bool attacked = false;

    WargState nowState;
    WargState nextState;
    
    // Use this for initialization
    void Start()
    {
        commonParameter = transform.root.GetComponent<WargsCommonParameter>();
        animator = GetComponent<WargAnimator>();
        searchController = transform.Find("SearchAreaTrigger").GetComponent<WargSearch>();

        // 初期位置を保持
        initPosition = transform.position;

        // 待機時間
        waitTime = commonParameter.MinWaitTime;
        NowHp = EnemyFunctionRef.GetEnemyParameter().MaxHp;
        nowState = WargState.Searching;
        nextState = WargState.Searching;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        animator.SetMoveAnimatorParameter();

        if (IsDied||IsFloating)
            return;

        switch (nowState)
        {
            case WargState.Searching:
                Searching();
                break;
            case WargState.Chasing:
                Chasing();
                break;
            case WargState.Attacking:
                Attacking();
                break;
        }

        if (nowState != nextState)
        {
            nowState = nextState;
            switch (nowState)
            {
                case WargState.Searching:
                    PrepareForSearch();
                    break;
                case WargState.Chasing:
                    PrepareForChase();
                    break;
                case WargState.Attacking:
                    PrepareForAttack();
                    break;
                case WargState.Died:
                    Died();
                    break;
            }
        }

    }


    // ステートを変更する.
    void ChangeState(WargState _nextState)
    {
        nextState = _nextState;
    }
    //徘徊準備
    void PrepareForSearch()
    {
        ResetStateFlag();
    }
    void Searching()
    {
        // ターゲットを発見したら追跡
        if (AttackTarget)
        {
            ChangeState(WargState.Chasing);
            //waitTime = 0;
        }
        // 待機時間がまだあったら
        if (waitTime > 0.0f)
        {
            // 待機時間を減らす
            waitTime -= Time.deltaTime;
            // 待機時間が無くなったら
            if (waitTime <= 0.0f)
            {
                // 範囲内の何処か
                Vector2 randomValue = Random.insideUnitCircle * commonParameter.WalkRange;
                // 移動先の設定
                Vector3 destinationPosition = initPosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                //　目的地の指定.
                EnemyFunctionRef.GetEnemyMove().SetDestination(destinationPosition);
            }
        }
        else
        {
            searchController.SearchByEye();

            // 目的地へ到着
            if (EnemyFunctionRef.GetEnemyMove().CheckIsArrived())
            {
                // 待機状態へ
                waitTime = Random.Range(commonParameter.MinWaitTime, commonParameter.MaxWaitTime);
            }
        }
    }


    // 追跡準備
    void PrepareForChase()
    {
        ResetStateFlag();
    }
    // 追跡中
    void Chasing()
    {
        // 移動先をプレイヤーに設定
        EnemyFunctionRef.GetEnemyMove().SetDestination(AttackTarget.position);

        // FIXME:攻撃範囲内に近づいたら攻撃、攻撃範囲の値がマジックナンバーなってる
        if (Vector3.Distance(AttackTarget.position, transform.position) <= 2.8f)
        {
            ChangeState(WargState.Attacking);
        }
    }

    // 攻撃準備
    void PrepareForAttack()
    {
        ResetStateFlag();
        attacking = true;

        // 敵の方向に振り向かせる.
        Vector3 targetDirection = (AttackTarget.position - transform.position).normalized;
        EnemyFunctionRef.GetEnemyMove().SetDirectionXZ(targetDirection);

        // 移動を止める.
        EnemyFunctionRef.GetEnemyMove().StopMove();
    }
    // 攻撃中
    void Attacking()
    {
        // 移動を止める.
        EnemyFunctionRef.GetEnemyMove().StopMove();
        //TODO:ここに攻撃の処理を入れる
        if (AttackTarget.GetComponent<PlayerStatus>().nowHp <= 0)
        {
            searchController.Research();
        }
        ChangeState(WargState.Searching);
    }

    void dropItem()
    {
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    void Died()
    {
        dropItem();
        Destroy(gameObject);
    }
    /// <summary>
    /// ステートが始まる前にステータスﾌﾗｸﾞを初期化する.
    /// FIXME:名前がおかしい
    /// </summary>
    void ResetStateFlag()
    {
        attacking = false;
    }

    /// <summary>
    /// 攻撃状態を取得する
    /// </summary>
    public bool GetAttackedFlag()
    {
        return attacked;
    }

    /// <summary>
    /// 攻撃完了
    /// </summary>
    public void TurnOffAttackedFlag()
    {
        attacked = false;
    }

    /// <summary>
    /// 攻撃未完了
    /// </summary>
    public void TurnOnAttackedFlag()
    {
        attacked = false;
    }


}
