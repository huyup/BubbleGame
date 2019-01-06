using System.Collections;
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
    private float spaceKeySpeed = 0.5f;

    private Vector3 bubbleStartPos;
    private Rigidbody rb;

    private PlayerController playerController;

    private PlayerStatus playerStatus;

    private float spaceStorage;

    [SerializeField]
    private int minShootCost = 10;

    private float tmpAmmoCost;

    private float nowAmmoLeft;
    private float prevAmmoLeft;

    private bool canAttack = false;
    // Use this for initialization
    void Start()
    {
        prevAmmoLeft = MaxAmmo;
        nowAmmoLeft = MaxAmmo;
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
    public override void OnAttackButtonDown()
    {
        if (nowAmmoLeft < minShootCost)
            return;
        playerController.BanMove();
        spaceStorage = 0;
        canAttack = true;
        tmpAmmoCost = minShootCost;
        nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;
    }
    public override void OnAttackButtonStay()
    {
        if (!canAttack)
            return;



        if (spaceStorage < airGunMaxPower)
        {
            playerController.BanGravity();
            spaceStorage += spaceKeySpeed * Time.fixedDeltaTime * 60;
        }
        else
        {
            //最大になったら、自動的に前へ出す
            OnAttackButtonUp();
        }


        if (nowAmmoLeft <= 0)
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
        spaceStorage = 0;
        prevAmmoLeft = nowAmmoLeft;

        canAttack = false;
        playerController.ResetMove();
        playerController.ResetGravity();
        //使い切ったら、禁止させる
        if (nowAmmoLeft <= 0)
        {
            playerController.DisableAirGun();
        }

    }
    public override void OnReset()
    {
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
