using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeaponA : PlayerWeapon
{
    /// <summary>
    /// 泡のオブジェs
    /// </summary>
    [SerializeField]
    private GameObject bubbleSet;

    [SerializeField]
    private GameObject weaponAStartRef;

    private List<GameObject> bubbles = new List<GameObject>();

    private GameObject bubbleStartObj;
    private Vector3 bubbleStartPos;

    private Rigidbody rb;

    private PlayerStatus status;
    private PlayerController controller;
    private BubbleProperty bubbleProperty;

    private float spaceKeyStorage = 0.0f;
    private bool isPushed = false;

    [SerializeField]
    private int minShootCost = 10;

    [SerializeField]
    private float buttonStayCost = 0.3f;

    [FormerlySerializedAs("maxShootCost")]
    [SerializeField]
    private int maxAmmoCost = 30;

    private float tmpAmmoCost;

    private float nowAmmoLeft;
    private float prevAmmoLeft;

    private bool isAttacking = false;
    // Use this for initialization
    void Start()
    {
        prevAmmoLeft = MaxAmmo;
        nowAmmoLeft = MaxAmmo;

        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
        bubbleProperty = bubbleSet.transform.Find("Bubble").GetComponent<BubbleProperty>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("tmpAmmoCost" + tmpAmmoCost);
        Debug.Log("nowAmmoLeft" + nowAmmoLeft);

        if (nowAmmoLeft < MaxAmmo && !isAttacking)
        {
            prevAmmoLeft += ReloadSpeed;
            nowAmmoLeft += ReloadSpeed;
        }

        if (nowAmmoLeft < 0)
            nowAmmoLeft = 0;

        //泡の発射位置を更新させる
        bubbleStartPos = weaponAStartRef.transform.position;
    }
    public override int GetNowAmmo()
    {
        return (int)nowAmmoLeft;
    }
    public override void OnAttackButtonDown()
    {
        isAttacking = true;
        tmpAmmoCost = minShootCost;
        nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;

        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonDown();
        controller.BanMove();
        controller.BanJump();
        rb.velocity = Vector3.zero;

        isPushed = false;
        spaceKeyStorage = 0;

        GameObject bubbleSetInstance = Instantiate(bubbleSet) as GameObject;

        GameObject bubbleInstance = bubbleSetInstance.transform.Find("Bubble").gameObject;

        bubbles.Add(bubbleInstance);

        bubbles[bubbles.Count - 1].transform.position = bubbleStartPos;

    }

    public override void OnAttackButtonStay()
    {
        if (bubbles.Count == 0)
            return;
        isAttacking = true;
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonStay();

        if (bubbles[bubbles.Count - 1])
        {
            if (spaceKeyStorage < bubbleProperty.MaxSize)
            {
                nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;
                if (tmpAmmoCost < maxAmmoCost)
                {
                    tmpAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;
                }

                spaceKeyStorage += status.SpaceKeySpeed * Time.fixedDeltaTime;
                bubbles[bubbles.Count - 1].transform.localScale += new Vector3(spaceKeyStorage, spaceKeyStorage, spaceKeyStorage);
                //少しずつ前に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.forward * Time.fixedDeltaTime * 1.2f;
                //少しずつ上に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.up * Time.fixedDeltaTime * 0.8f;
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
        isAttacking = false;
        controller.ResetJump();
        controller.ResetMove();
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonUp();
        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }

        prevAmmoLeft = nowAmmoLeft;
    }
    private void PushTheBubbleOnceTime()
    {
        if (bubbles.Count == 0)
            return;

        if (bubbles[bubbles.Count - 1] == null || isPushed)
            return;

        if (!bubbles[bubbles.Count - 1].GetComponent<BubbleProperty>().IsForceFloating)
        {
            bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.forward * status.BubbleFowardPower,
                ForceMode.VelocityChange);
            bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.up * status.BubbleUpPower,
                ForceMode.VelocityChange);
        }
        bubbles[bubbles.Count - 1].GetComponent<BubbleCollision>().SetDestroyEnable();
    }

    public override void OnReset()
    {
        base.OnReset();
        controller.ResetJump();
        controller.ResetMove();
        controller.ResetAttack();
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonUp();
        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }
    }
}
