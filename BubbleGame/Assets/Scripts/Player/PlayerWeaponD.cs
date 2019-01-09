using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponD : PlayerWeapon
{
    [SerializeField]
    private GameObject tornadoEff;

    [SerializeField]
    private GameObject tornadoTrigger;

    [SerializeField]
    private GameObject weaponDStartRef;

    private Vector3 bubbleStartPos;
    private Rigidbody rb;

    private PlayerController playerController;

    [SerializeField]
    private int minShootCost = 10;

    private float tmpAmmoCost;

    private float nowAmmoLeft;
    private float prevAmmoLeft;

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
        //泡の発射位置を更新させる
        bubbleStartPos = weaponDStartRef.transform.position;
    }

    public void Reload()
    {
        prevAmmoLeft = MaxAmmo;
        nowAmmoLeft = MaxAmmo;
        tmpAmmoCost = 0;
    }
    public override int GetNowAmmo()
    {
        return (int)nowAmmoLeft;
    }
    public override void OnAttackButtonDown()
    {
        if (nowAmmoLeft < minShootCost)
            return;

        playerController.BanMove();
        playerController.BanJump();
        tmpAmmoCost = minShootCost;
        nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;

        tornadoEff.transform.position = bubbleStartPos;
        tornadoEff.GetComponent<ParticleSystem>().Play();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().TakeObjIn();

    }
    public override void OnAttackButtonStay()
    {
        playerController.BanJump();
        playerController.BanMove();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().TakeObjIn();
    }
    public override void OnAttackButtonUp()
    {
        playerController.ResetJump();
        playerController.ResetMove();
        tornadoEff.GetComponent<ParticleSystem>().Clear();
        tornadoEff.GetComponent<ParticleSystem>().Stop();

        prevAmmoLeft = nowAmmoLeft;
    }
    public override void OnReset()
    {
    }

}
