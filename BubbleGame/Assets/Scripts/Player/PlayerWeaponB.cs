using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponB : PlayerWeapon
{
    /// <summary>
    /// 泡のオブジェ
    /// </summary>
    [SerializeField] private GameObject rapidFireBubble;

    [SerializeField] private GameObject weaponBStartRef;

    private PlayerController playerController;
    private Vector3 bubbleStartPos;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = weaponBStartRef.transform.position;
    }
    public override void OnAttackButtonDown()
    {
        rb.velocity = Vector3.zero;
        playerController.BanJump();
        OnCreateBubble();
    }

    public override void OnAttackButtonStay()
    {
        playerController.BanJump();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    public override void OnAttackButtonUp()
    {
        playerController.ResetJump();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
    }

    private void OnCreateBubble()
    {
        playerController.ResetJump();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
        rapidFireBubble.GetComponent<ParticleSystem>().Play();
    }

    public override void OnReset()
    {
        base.OnReset();
        playerController.ResetAttack();
        playerController.ResetJump();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
    }
}
