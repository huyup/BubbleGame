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

    private PlayerController playerController;

    [SerializeField]
    private int minShootCost = 10;

    private float tmpAmmoCost;

    private float prevAmmoLeft;
    private PlayerAmmoCtr playerAmmoCtr;
    // Use this for initialization
    void Start()
    {
        playerAmmoCtr = GetComponent<PlayerAmmoCtr>();
        playerController = GetComponent<PlayerController>();

        prevAmmoLeft = playerAmmoCtr.MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = weaponDStartRef.transform.position;
    }
    public override void OnAttackButtonDown()
    {
        if (playerAmmoCtr.NowAmmoLeft < minShootCost)
            return;

        playerController.BanMove();
        playerController.BanJump();
        tmpAmmoCost = minShootCost;
        playerAmmoCtr.NowAmmoLeft = prevAmmoLeft - tmpAmmoCost;

        tornadoEff.transform.position = bubbleStartPos;

        tornadoEff.GetComponent<ParticleSystem>().Play();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().TakeObjIn();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().ResetTriggerRadius();
    }
    public override void OnAttackButtonStay()
    {

        playerController.BanJump();
        playerController.BanMove();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().TakeObjIn();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().IncreaseTriggerRadius();
    }
    public override void OnAttackButtonUp()
    {
        playerController.ResetJump();
        playerController.ResetMove();
        tornadoEff.GetComponent<ParticleSystem>().Clear();
        tornadoEff.GetComponent<ParticleSystem>().Stop();

        tornadoTrigger.GetComponent<TornadoTriggerCtr>().ResetTriggerRadius();
        prevAmmoLeft = playerAmmoCtr.NowAmmoLeft;
    }
    public override void OnChange()
    {
    }

}
