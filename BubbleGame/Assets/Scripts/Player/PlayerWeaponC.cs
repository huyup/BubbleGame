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

    private bool canShoot = false;
    // Use this for initialization
    void Start()
    {
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
        if (!airGun.GetComponent<ParticleSystem>().isPlaying)
        {
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
