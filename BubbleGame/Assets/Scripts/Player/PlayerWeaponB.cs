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

    [SerializeField]
    private int minShootCost = 10;

    [SerializeField]
    private float buttonStayCost = 0.3f;

    [SerializeField]
    private float factorToCalAmmoRecovery = 1.5f;

    private float tmpAmmoCost;

    private float prevAmmoLeft;

    private bool isAttacking = false;

    private PlayerAmmoCtr playerAmmoCtr;
    // Use this for initialization
    void Start()
    {

        playerAmmoCtr = GetComponent<PlayerAmmoCtr>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();

        prevAmmoLeft = playerAmmoCtr.MaxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        //Reload();

        //泡の発射位置を更新させる
        bubbleStartPos = weaponBStartRef.transform.position;
    }

    //private void Reload()
    //{
    //    if (playerAmmoCtr.NowAmmoLeft < playerAmmoCtr.MaxAmmo && !isAttacking)
    //    {
    //        prevAmmoLeft += ReloadSpeed;
    //        playerAmmoCtr.NowAmmoLeft += ReloadSpeed;
    //    }
    //}
    public override void OnAttackButtonDown()
    {
        if (playerAmmoCtr.NowAmmoLeft < minShootCost)
            return;

        isAttacking = true;
        tmpAmmoCost = minShootCost;
        playerAmmoCtr.NowAmmoLeft = prevAmmoLeft - tmpAmmoCost;

        rb.velocity = Vector3.zero;
        playerController.BanJump();
        OnCreateBubble();
    }

    public override void OnAttackButtonStay()
    {
        if (playerAmmoCtr.NowAmmoLeft <= 0)
            OnAttackButtonUp();
        isAttacking = true;
        tmpAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;
        playerAmmoCtr.NowAmmoLeft = prevAmmoLeft - tmpAmmoCost;


        playerController.BanJump();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    public override void OnAttackButtonUp()
    {
        playerController.ResetJump();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();

        isAttacking = false;
        prevAmmoLeft = playerAmmoCtr.NowAmmoLeft;
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
