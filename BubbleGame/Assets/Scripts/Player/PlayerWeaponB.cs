using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponB : PlayerWeapon
{
    /// <summary>
    /// 泡のオブジェ
    /// </summary>
    [SerializeField]
    private GameObject bubbleSet;
    private List<GameObject> bubbles = new List<GameObject>();

    private GameObject bubbleStartObj;
    private Vector3 bubbleStartPos;

    private Rigidbody rb;

    private PlayerStatus status; 
    
    private bool isPushed = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();

        bubbleStartObj = transform.Find("BubbleStartObj").gameObject;
        
    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = bubbleStartObj.transform.position;
    }
    public override void OnAttackButtonDown()
    {
        base.OnAttackButtonDown();
        rb.velocity = Vector3.zero;

        isPushed = false;

        GameObject bubbleSetInstance = Instantiate(bubbleSet) as GameObject;

        GameObject bubbleInstance = bubbleSetInstance.transform.Find("Bubble").gameObject;

        bubbles.Add(bubbleInstance);

        bubbles[bubbles.Count - 1].transform.position = bubbleStartPos;

    }

    public override void OnAttackButtonStay()
    {
        if (bubbles.Count == 0)
            return;

        base.OnAttackButtonStay();


        //最大値を超えたら、自動的に前へ出す
        if (!isPushed)
        {
            PushTheBubbleOnceTime();
            isPushed = true;
        }
    }

    public override void OnAttackButtonUp()
    {
        base.OnAttackButtonUp();
        PushTheBubbleOnceTime();
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
            bubbles[bubbles.Count - 1].GetComponent<BubbleCollision>().SetDestroyEnable();
            isPushed = true;
        }
    }
}
