using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponA : PlayerWeapon
{
    /// <summary>
    /// 泡のオブジェs
    /// </summary>
    [SerializeField]
    private GameObject bubbleSet;

    [SerializeField] private GameObject weaponAStartRef;

    private List<GameObject> bubbles = new List<GameObject>();

    private GameObject bubbleStartObj;
    private Vector3 bubbleStartPos;

    private Rigidbody rb;

    private PlayerStatus status;
    private PlayerController controller;
    private BubbleProperty bubbleProperty;

    private float spaceKeyStorage = 0.0f;
    private bool isPushed = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
        bubbleProperty = bubbleSet.transform.Find("Bubble").GetComponent<BubbleProperty>();
    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = weaponAStartRef.transform.position;
    }
    public override void OnAttackButtonDown()
    {
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
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonStay();

        if (bubbles[bubbles.Count - 1])
        {
            if (spaceKeyStorage < bubbleProperty.MaxSize)
            {
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
        controller.ResetJump();
        controller.ResetMove();
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonUp();
        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }
    }
    private void PushTheBubbleOnceTime()
    {
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

    public override void Reset()
    {
        base.Reset();
        controller.ResetJump();
        controller.ResetMove();
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonUp();
        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }
    }
}
