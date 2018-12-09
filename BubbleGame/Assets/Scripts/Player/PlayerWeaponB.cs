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
    
    private float tmpAmmoCost;

    private float nowAmmoLeft;
    private float prevAmmoLeft;

    private bool isAttacking = false;
    // Use this for initialization
    void Start()
    {
        prevAmmoLeft = MaxAmmo;
        nowAmmoLeft = MaxAmmo;

        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Reload();

        if (nowAmmoLeft < 0)
            nowAmmoLeft = 0;

        //泡の発射位置を更新させる
        bubbleStartPos = weaponBStartRef.transform.position;
    }

    private void Reload()
    {
        if (nowAmmoLeft < MaxAmmo && !isAttacking)
        {
            prevAmmoLeft += ReloadSpeed;
            nowAmmoLeft += ReloadSpeed;
        }
    }
    public override int GetNowAmmo()
    {
        return (int)nowAmmoLeft;
    }
    public override void OnAttackButtonDown()
    {
        if (nowAmmoLeft < minShootCost)
            return;

        isAttacking = true;
        tmpAmmoCost = minShootCost;
        nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;


        rb.velocity = Vector3.zero;
        playerController.BanJump();
        OnCreateBubble();
    }

    public override void OnAttackButtonStay()
    {
        if (nowAmmoLeft <= 0)
            OnAttackButtonUp();
        isAttacking = true;
        tmpAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;
        nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;


        playerController.BanJump();
        rapidFireBubble.transform.position = bubbleStartPos;
        rapidFireBubble.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    public override void OnAttackButtonUp()
    {
        playerController.ResetJump();
        rapidFireBubble.GetComponent<ParticleSystem>().Stop();

        isAttacking = false;
        prevAmmoLeft = nowAmmoLeft;
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
