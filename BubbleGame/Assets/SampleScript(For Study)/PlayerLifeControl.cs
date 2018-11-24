using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface IDamageReceivable
{
    void ReceiveDamage(int damage);
}

/// <summary>
/// これはプレイヤーのライフをコントロールするクラス
/// </summary>
public class PlayerLifeControl : MonoBehaviour, IDamageReceivable
{

    public int P_DeadCount { set; get; }
    public int lifeCount;
    bool invincble = false;

    GameObject Eff_Invincble;
    GameObject soul;
    GameObject frontWall;


    void Start()
    {
        //ライフ初期化
        lifeCount = 1;

        Eff_Invincble = GameObject.Find("Eff_PowerUp_Fix");

        soul = GameObject.Find("Soul");

        frontWall = GameObject.Find("FrontWall");
    }
    private void ResetLife()
    {
        lifeCount = 1;
        invincble = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (lifeCount <= 0)
        {
            lifeCount = 0;
        }
    }
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void ReceiveDamage(int damage)
    {
        Debug.Log(string.Format("プレイヤーは {0} のダメージを受けた！", damage));
        
        lifeCount--;
        P_DeadCount++;
        //soul.GetComponent<DeadPerformanceScript>().SetCircleParameter(transform.position);
        invincble = true;
        transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
    }
}
