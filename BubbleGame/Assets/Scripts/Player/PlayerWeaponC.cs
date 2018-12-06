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
    // Use this for initialization
    void Start()
    {
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
        rb.velocity=Vector3.zero;
        airGun.transform.position = bubbleStartPos;
        airGun.transform.rotation = Quaternion.LookRotation(transform.forward);
        airGun.GetComponent<ParticleSystem>().Play();
    }

    public override void OnAttackButtonStay()
    {
        if(!airGun.GetComponent<ParticleSystem>().isPlaying)
            airGun.GetComponent<ParticleSystem>().Play();
    }

    public override void OnAttackButtonUp()
    {
        airGun.GetComponent<ParticleSystem>().Stop();
    }
    public override void OnReset()
    {
    }
}
