using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoCtr : MonoBehaviour
{
    [SerializeField]
    private float MaxAmmo = 100;

    [SerializeField]
    private int minShootCost = 10;

    [SerializeField]
    private float buttonStayCost = 0.3f;

    [SerializeField]
    private int maxAmmoCost = 30;

    [SerializeField]
    private float factorToCalAmmoRecovery = 1.5f;

    [SerializeField]
    private float reloadSpeed;

    private float storeAmmoCost;
    private float prevAmmoLeft;
    public float Ammo { get; set; }

    private PlayerStatus playerStatus;

    private PlayerController playerController;
    // Use this for initialization
    void Start()
    {
        Ammo = MaxAmmo;
        prevAmmoLeft = MaxAmmo;
        playerStatus = GetComponent<PlayerStatus>();

        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ammo < 0)
            Ammo = 0;

        if (playerStatus.WeaponSelection == WeaponSelection.Bubble
            && !playerController.GetWeapon().GetIsAttacking())
        {
            Reload();
        }
    }

    public float GetMinShootCost()
    {
        return minShootCost;
    }

    private void Reload()
    {
        if (Ammo < MaxAmmo)
        {
            prevAmmoLeft += reloadSpeed;
            Ammo += reloadSpeed;
        }
    }

    public void OnButtonDown()
    {
        storeAmmoCost = minShootCost;
        Ammo = prevAmmoLeft - storeAmmoCost;
    }

    public void OnButtonStay()
    {
        if (playerController.GetNowWeaponType() == WeaponType.WeaponA)
        {
            //弾薬計算
            if (storeAmmoCost < maxAmmoCost)
            {
                storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;
            }

            Ammo = prevAmmoLeft - storeAmmoCost;
        }
        else if (playerController.GetNowWeaponType() == WeaponType.WeaponB)
        {
            storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;

            Ammo = prevAmmoLeft - storeAmmoCost;
        }
        else if (playerController.GetNowWeaponType() == WeaponType.WeaponC)
        {
        }
        else if (playerController.GetNowWeaponType() == WeaponType.WeaponD)
        {
            storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;

            Ammo = prevAmmoLeft - storeAmmoCost;
        }
    }

    public bool CanShoot()
    {
        if (Ammo >= minShootCost)
            return true;
        else
            return false;
    }
    public void OnButtonUp()
    {
        prevAmmoLeft = Ammo;
    }
    public void AmmoRecovery(float _bubbleSize)
    {
        Ammo += _bubbleSize * factorToCalAmmoRecovery;
        if (Ammo >= MaxAmmo)
            Ammo = MaxAmmo;
    }
    public int GetNowAmmo()
    {
        return (int)Ammo;
    }
}
