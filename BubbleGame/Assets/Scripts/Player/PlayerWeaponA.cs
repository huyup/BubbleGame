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

    private float spaceKeyStorage = 0.0f;

    #endregion

    #region 消費用
    private bool isPushed = false;

    private bool isAttacking = false;

    private PlayerAmmoCtr ammoCtr;
    #endregion

    void Start()
    {
        animatorCtr = GetComponent<PlayerAnimatorCtr>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
        bubbleProperty = bubbleSet.transform.Find("Bubble").GetComponent<BubbleProperty>();
        ammoCtr = GetComponent<PlayerAmmoCtr>();
    }

    void Update()
    {
        ////泡の発射位置を更新させる
        bubbleStartPos = weaponAStartRef.transform.position;
    }

    public override void OnAttackButtonDown()
    {
        if (ammoCtr.Ammo < ammoCtr.GetMinShootCost())
            return;

        ammoCtr.OnButtonDown();

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
        bubbles[bubbles.Count - 1].GetComponent<BubbleController>().PlayerSelectionWhoCreated =
            GetComponent<PlayerStatus>().PlayerSelection;
    }

    public override void OnAttackButtonStay()
    {
        if (isPushed)
            return;

        if (bubbles.Count == 0)
        {
            OnAttackButtonUp();
            return;
        }

        if (ammoCtr.Ammo <= 0)
        {
            StartCoroutine(DelayReset());
            return;
        }

        isAttacking = true;
        animatorCtr.SetAttackAnimationOnButtonStay();

        if (bubbles[bubbles.Count - 1])
        {
            if (spaceKeyStorage < bubbleProperty.MaxSize)
            {
                ammoCtr.OnButtonStay();
                spaceKeyStorage += status.SpaceKeySpeed * Time.fixedDeltaTime;
                bubbles[bubbles.Count - 1].transform.localScale += new Vector3(spaceKeyStorage, spaceKeyStorage, spaceKeyStorage);
                //少しずつ前に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.forward * Time.fixedDeltaTime * offsetForceToCalculateFront;
                //少しずつ上に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.up * Time.fixedDeltaTime * offsetForceToCalculateHeight;
            }
            else
            {
                StartCoroutine(DelayReset());
            }
        }
        else
        {
            OnAttackButtonUp();
        }

    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(0.3f);
        OnAttackButtonUp();
    }
    public override void OnAttackButtonUp()
    {
        controller.ResetJump();
        controller.ResetMove();
        animatorCtr.SetAttackAnimationOnButtonUp();
        ammoCtr.OnButtonUp();

        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }

        isAttacking = false;

    }



    public override void OnChangeWeapon()
    {
        base.OnChangeWeapon();
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

    public override bool GetIsAttacking()
    {
        return isAttacking;
    }


    private void PushTheBubbleOnceTime()
    {
        if (bubbles.Count == 0)
            return;

        if (bubbles[bubbles.Count - 1] == null || isPushed)
            return;

        bubbles[bubbles.Count - 1].GetComponent<BubbleController>().SetBubbleState(BubbleState.StandBy);

        bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.forward * status.BubbleForwardPower);
        bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.up * status.BubbleUpPower);

    }


}
