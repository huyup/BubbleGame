using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponC : PlayerWeapon
{
    [SerializeField]
    private GameObject airGun;

    [SerializeField]
    private GameObject weaponCStartRef;

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
    public override void OnAttackButtonDown()
    {
        if (nowAmmoLeft < minShootCost)
            return;
        if (!airGun.GetComponent<ParticleSystem>().isPlaying)
        {
            tmpAmmoCost = minShootCost;
            nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;

            spaceStorage = 0;
            rb.velocity = Vector3.zero;
            airGun.transform.position = bubbleStartPos;
            airGun.transform.rotation = Quaternion.LookRotation(transform.forward);
            ParticleSystem[] particleSystems = airGun.GetComponentsInChildren<ParticleSystem>();

            foreach (var particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
            playerController.PullBack();
            StartCoroutine(PullBack());
        }
    }

    public override void OnAttackButtonStay()
    {
    }

    public override void OnAttackButtonUp()
    {
        airGun.GetComponent<ParticleSystem>().Stop();
        prevAmmoLeft = nowAmmoLeft;

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
        playerController.ResetMove();
        playerController.ResetRotate();
        playerController.ResetAttack();
        playerController.ResetJump();

    }
}
