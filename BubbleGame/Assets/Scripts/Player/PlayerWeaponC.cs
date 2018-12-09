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

    [SerializeField]
    private int minShootCost = 10;

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
        playerStatus = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Reload();

        if (nowAmmoLeft < 0)
            nowAmmoLeft = 0;
        //泡の発射位置を更新させる
        bubbleStartPos = weaponCStartRef.transform.position;
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
        if (!airGun.GetComponent<ParticleSystem>().isPlaying)
        {
            isAttacking = true;
            tmpAmmoCost = minShootCost;
            nowAmmoLeft = prevAmmoLeft - tmpAmmoCost;
            rb.velocity = Vector3.zero;
            airGun.transform.position = bubbleStartPos;
            airGun.transform.rotation = Quaternion.LookRotation(transform.forward);
            ParticleSystem[] particleSystems = airGun.GetComponentsInChildren<ParticleSystem>();
            foreach (var particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
            //airGun.GetComponent<ParticleSystem>().Play();
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

        isAttacking = false;
        prevAmmoLeft = nowAmmoLeft;
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
