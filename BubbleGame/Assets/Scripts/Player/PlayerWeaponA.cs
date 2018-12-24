using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeaponA : PlayerWeapon
{
    #region 発射用
    [SerializeField]
    private GameObject bubbleSet;

    [SerializeField]
    private GameObject weaponAStartRef;

    [SerializeField]
    private float offsetForceToCalculateHeight = 0.8f;

    [SerializeField]
    private float offsetForceToCalculateFront = 1.2f;

    private List<GameObject> bubbles = new List<GameObject>();

    private Rigidbody rb;
    private PlayerStatus status;
    private PlayerAnimatorCtr animatorCtr;
    private PlayerController controller;
    private BubbleProperty bubbleProperty;
    private Vector3 bubbleStartPos;
    #endregion

    #region 消費用
    [SerializeField]
    private int minShootCost = 10;

    [SerializeField]
    private float buttonStayCost = 0.3f;

    [SerializeField]
    private int maxAmmoCost = 30;

    private float spaceKeyStorage = 0.0f;

    private bool isPushed = false;

    private bool isAttacking = false;

    private float storeAmmoCost;

    private float nowAmmoLeft;

    private float prevAmmoLeft;
    #endregion
    
    void Start()
    {
        prevAmmoLeft = MaxAmmo;
        nowAmmoLeft = MaxAmmo;

        animatorCtr = GetComponent<PlayerAnimatorCtr>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
        bubbleProperty = bubbleSet.transform.Find("Bubble").GetComponent<BubbleProperty>();
    }

    void Update()
    {
        Reload();

        if (nowAmmoLeft < 0)
            nowAmmoLeft = 0;
        
        ////泡の発射位置を更新させる
        bubbleStartPos = weaponAStartRef.transform.position;
    }

    public override void OnAttackButtonDown()
    {
        if (nowAmmoLeft < minShootCost)
            return;

        isAttacking = true;
        storeAmmoCost = minShootCost;
        nowAmmoLeft = prevAmmoLeft - storeAmmoCost;

        //発射
        animatorCtr.SetAttackAnimationOnButtonDown();
        controller.BanMove();
        controller.BanJump();
        rb.velocity = Vector3.zero;

        isPushed = false;
        spaceKeyStorage = 0;

        GameObject bubbleSetInstance = Instantiate(bubbleSet) as GameObject;

        GameObject bubbleInstance = bubbleSetInstance.transform.Find("Bubble").gameObject;

        bubbles.Add(bubbleInstance);

        bubbles[bubbles.Count - 1].transform.position = bubbleStartPos;

        bubbles[bubbles.Count - 1].GetComponent<BubbleController>().SetBubbleState(BubbleState.Creating);
    }

    public override void OnAttackButtonStay()
    {
        if (bubbles.Count == 0)
            return;

        if (nowAmmoLeft <= 0)
        {
            //残量が足りなかったら、自動的に前へ出す
            if (!isPushed)
            {
                PushTheBubbleOnceTime();
                isPushed = true;
            }
            return;
        }

        isAttacking = true;
        animatorCtr.SetAttackAnimationOnButtonStay();

        if (bubbles[bubbles.Count - 1])
        {
            if (spaceKeyStorage < bubbleProperty.MaxSize)
            {
                //弾薬計算
                if (storeAmmoCost < maxAmmoCost)
                {
                    storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;
                }
                nowAmmoLeft = prevAmmoLeft - storeAmmoCost;
                spaceKeyStorage += status.SpaceKeySpeed * Time.fixedDeltaTime;
                bubbles[bubbles.Count - 1].transform.localScale += new Vector3(spaceKeyStorage, spaceKeyStorage, spaceKeyStorage);
                //少しずつ前に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.forward * Time.fixedDeltaTime * offsetForceToCalculateFront;
                //少しずつ上に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.up * Time.fixedDeltaTime * offsetForceToCalculateHeight;
            }
            else
            {
                //最大値を超えたら、自動的に前へ出す
                if (!isPushed)
                {
                    PushTheBubbleOnceTime();
                    isPushed = true;
                }
            }
        }
    }

    public override void OnAttackButtonUp()
    {
        controller.ResetJump();
        controller.ResetMove();
        animatorCtr.SetAttackAnimationOnButtonUp();


        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }

        isAttacking = false;
        prevAmmoLeft = nowAmmoLeft;
    }

    public override int GetNowAmmo()
    {
        return (int)nowAmmoLeft;
    }

    public override void OnReset()
    {
        base.OnReset();
        controller.ResetJump();
        controller.ResetMove();
        controller.ResetAttack();
        animatorCtr.SetAttackAnimationOnButtonUp();
        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }
    }

    private void Reload()
    {
        if (nowAmmoLeft < MaxAmmo && !isAttacking)
        {
            prevAmmoLeft += ReloadSpeed;
            nowAmmoLeft += ReloadSpeed;
        }
    }

    private void PushTheBubbleOnceTime()
    {
        if (bubbles.Count == 0)
            return;

        if (bubbles[bubbles.Count - 1] == null || isPushed)
            return;

        bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.forward * status.BubbleFowardPower);
        bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.up * status.BubbleUpPower);

        bubbles[bubbles.Count - 1].GetComponent<BubbleController>().SetBubbleState(BubbleState.StandBy);
    }


}
