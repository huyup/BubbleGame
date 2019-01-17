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

    private PlayerAmmoCtr ammoCtr;
    // Use this for initialization
    void Start()
    {
        ammoCtr = GetComponent<PlayerAmmoCtr>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = weaponDStartRef.transform.position;
    }
    public override void OnAttackButtonDown()
    {
        if (ammoCtr.Ammo < ammoCtr.GetMinShootCost())
            return;
        ammoCtr.OnButtonDown();
        playerController.BanMove();
        playerController.BanJump();
        tornadoEff.transform.position = bubbleStartPos;

        tornadoEff.GetComponent<ParticleSystem>().Play();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().TakeObjIn();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().ResetTriggerRadius();
    }
    public override void OnAttackButtonStay()
    {
        ammoCtr.OnButtonStay();
        playerController.BanJump();
        playerController.BanMove();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().TakeObjIn();
        tornadoTrigger.GetComponent<TornadoTriggerCtr>().IncreaseTriggerRadius();
    }
    public override void OnAttackButtonUp()
    {
        ammoCtr.OnButtonUp();
        playerController.ResetJump();
        playerController.ResetMove();
        tornadoEff.GetComponent<ParticleSystem>().Clear();
        tornadoEff.GetComponent<ParticleSystem>().Stop();

        tornadoTrigger.GetComponent<TornadoTriggerCtr>().ResetTriggerRadius();
    }
    public override void OnChange()
    {
    }

}
