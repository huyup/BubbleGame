using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponB : PlayerWeapon
{
    /// <summary>
    /// 泡のオブジェ
    /// </summary>
    [SerializeField] private GameObject rapidFireBubble;

    private GameObject bubbleStartObj;
    private Vector3 bubbleStartPos;

    private Rigidbody rb;

    private PlayerStatus status;
    private PlayerController controller;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();

        bubbleStartObj = transform.Find("BubbleStartObj2").gameObject;

    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = bubbleStartObj.transform.position;
    }
    public override void OnAttackButtonDown()
    {
        rb.velocity = Vector3.zero;

        OnCreateBubble();
    }

    public override void OnAttackButtonStay()
    {
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    public override void OnAttackButtonUp()
    {
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
    }

    private void OnCreateBubble()
    {
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
        rapidFireBubble.GetComponent<ParticleSystem>().Play();
    }

    public override void Reset()
    {
        base.Reset();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
    }
}
