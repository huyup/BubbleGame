using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponB : PlayerWeapon
{
    /// <summary>
    /// 泡のオブジェ
    /// </summary>
    [SerializeField]
    private GameObject rapidFireBubble;

    [SerializeField]
    private GameObject weaponBStartRef;

    private PlayerController playerController;

    private Vector3 bubbleStartPos;

    private Rigidbody rb;

    private bool isAttacking = false;

    private PlayerAmmoCtr ammoCtr;
    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();

        prevAmmoLeft = playerAmmoCtr.MaxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = weaponBStartRef.transform.position;
    }

    public override void OnAttackButtonDown()
    {
        if (ammoCtr.Ammo < ammoCtr.GetMinShootCost())
            return;

        isAttacking = true;
        ammoCtr.OnButtonDown();
        rb.velocity = Vector3.zero;
        playerController.BanJump();
        OnCreateBubble();
    }

    public override void OnAttackButtonStay()
    {
        if (ammoCtr.Ammo <= 0)
            OnAttackButtonUp();
        isAttacking = true;
        ammoCtr.OnButtonStay();
        playerController.BanJump();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    public override void OnAttackButtonUp()
    {
        playerController.ResetJump();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
        ammoCtr.OnButtonUp();
        isAttacking = false;
    }
    public override bool GetIsAttacking()
    {
        return isAttacking;
    }


    private void OnCreateBubble()
    {
        playerController.ResetJump();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
        rapidFireBubble.GetComponent<ParticleSystem>().Play();
    }

    public override void OnChange()
    {
        base.OnChange();
        playerController.ResetAttack();
        playerController.ResetJump();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();
    }
}
