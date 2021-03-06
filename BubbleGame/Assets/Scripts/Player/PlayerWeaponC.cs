﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponC : PlayerWeapon
{
    [SerializeField]
    private GameObject airGun;

    [SerializeField]
    private GameObject weaponCStartRef;

    [SerializeField]
    private float airGunMaxPower = 30;

    [SerializeField]
    private GameObject buttonStayEff;

    [SerializeField]
    private float spaceKeySpeed = 0.5f;

    [SerializeField]
    private BubbleDetectorLine bubbleDetectorLine;

    private Vector3 bubbleStartPos;
    private Rigidbody rb;

    private PlayerController playerController;

    private PlayerStatus playerStatus;

    private float spaceStorage;

    private bool canAttack = false;

    private PlayerAmmoCtr ammoCtr;
    // Use this for initialization
    void Start()
    {
        ammoCtr = GetComponent<PlayerAmmoCtr>();


        playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = weaponCStartRef.transform.position;
    }
    public override void OnAttackButtonDown()
    {
        ammoCtr.OnButtonDown();

        playerController.BanMove();
        spaceStorage = 0;
        canAttack = true;
        buttonStayEff.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        buttonStayEff.GetComponentInChildren<ParticleSystem>().Play();


    }
    public override void OnAttackButtonStay()
    {
        if (!canAttack)
            return;

        if (spaceStorage < airGunMaxPower)
        {
            playerController.BanGravity();
            ammoCtr.OnButtonStay();
            spaceStorage += spaceKeySpeed * Time.fixedDeltaTime * 60;
        }
        else
        {
            //最大になったら、自動的に前へ出す
            OnAttackButtonUp();
        }
        if (ammoCtr.Ammo <= 0)
        {
            //残量が足りなかったら、自動的に前へ出す
            OnAttackButtonUp();
        }
    }
    public override void OnAttackButtonUp()
    {
        if (!canAttack)
            return;

        Shoot(spaceStorage, airGunMaxPower);
        buttonStayEff.GetComponentInChildren<ParticleSystem>().Clear();
        buttonStayEff.GetComponentInChildren<ParticleSystem>().Stop();
        ammoCtr.OnButtonUp();
        spaceStorage = 0;
        canAttack = false;
        playerController.ResetMove();
        playerController.ResetGravity();
    }

    public void UseShootSupportLine()
    {
        //補助線を更新
        bubbleDetectorLine.CanUseShootSupportLine = true;
    }
    public override void OnChange()
    {
        //補助線を更新
        bubbleDetectorLine.CanUseShootSupportLine = false;
    }
    private void Shoot(float _power, float _maxPower)
    {
        airGun.GetComponent<AirGunCtr>().SetAirGunPower(_power);
        airGun.GetComponent<AirGunCtr>().SetAirGunParticle(_power, _maxPower);

        ParticleSystem[] particleSystems = airGun.GetComponentsInChildren<ParticleSystem>();
        if (!airGun.GetComponent<ParticleSystem>().isPlaying)
        {
            rb.velocity = Vector3.zero;
            airGun.transform.position = bubbleStartPos;
            airGun.transform.rotation = Quaternion.LookRotation(transform.forward);

            foreach (var particleSystem in particleSystems)
            {
                particleSystem.Play();
            }

            playerController.PullBack();
            StartCoroutine(PullBack());
        }
    }
    IEnumerator PullBack()
    {
        playerController.BanMove();
        playerController.BanRotate();
        playerController.BanJump();
        playerController.BanAttack();
        yield return new WaitForSeconds(playerStatus.PullBackTime);
        airGun.GetComponent<AirGunCtr>().SetAirGunPower(5);
        ParticleSystem[] particleSystems = airGun.GetComponentsInChildren<ParticleSystem>();
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
        playerController.ResetMove();
        playerController.ResetRotate();
        playerController.ResetAttack();
        playerController.ResetJump();
    }
}
