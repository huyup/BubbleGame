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
    // Use this for initialization
    void Start()
    {
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
        base.OnAttackButtonDown();
        rb.velocity = Vector3.zero;
        
        OnCreateBubble();
    }

    public override void OnAttackButtonStay()
    {
        base.OnAttackButtonStay();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    public override void OnAttackButtonUp()
    {
        base.OnAttackButtonUp();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
    }

    private void OnCreateBubble()
    {
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
        rapidFireBubble.GetComponent<ParticleSystem>().Play();
    }

    private void OnCreateBubbleInterval()
    {
    }
}
