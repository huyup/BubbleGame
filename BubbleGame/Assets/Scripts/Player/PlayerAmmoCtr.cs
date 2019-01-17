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
    private int minShootCostB = 2;

    [SerializeField]
    private float buttonStayCost = 0.3f;

    [SerializeField]
    private float buttonStayCostByTakeInMachine = 0.1f;

    [SerializeField]
    private int maxAmmoCost = 30;

    [SerializeField]
    private float factorToCalAmmoRecovery = 1.5f;

    [SerializeField]
    private float reloadSpeed;

    private float storeAmmoCost;
    private float prevAmmoLeft;
    public float Ammo { get; set; }


    private bool isUseUpAmmo = false;
    private PlayerStatus playerStatus;

    private PlayerController playerController;

    [SerializeField]
    private StageMain stageMain;
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
        if (Ammo <= 0)
        {
            if (playerStatus.WeaponSelection == WeaponSelection.Bubble)
            {
                Ammo = 0;
            }
            else
            {
                stageMain.CreateItemInRandomPoint();
                playerController.DisableAirGun();
            }
        }
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
            Debug.Log("Reload");
            prevAmmoLeft += reloadSpeed;
            Ammo += reloadSpeed;
            Debug.Log("Ammo" + Ammo);
        }
    }

    public void OnButtonDown()
    {
        if (playerController.GetNowWeaponType() == WeaponType.WeaponA)
        {
            storeAmmoCost = minShootCost;
            if (Ammo > 0)
                Ammo = prevAmmoLeft - storeAmmoCost;
            else
            {
                Ammo = 0;
                prevAmmoLeft = 0;
            }
        }
        else
        {
            storeAmmoCost = minShootCostB;
            if (Ammo > 0)
                Ammo = prevAmmoLeft - storeAmmoCost;
            else
            {
                Ammo = 0;
                prevAmmoLeft = 0;
            }
        }
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

            if (Ammo > 0)
                Ammo = prevAmmoLeft - storeAmmoCost;
            else
            {
                Ammo = 0;
                prevAmmoLeft = 0;
            }
        }
        else if (playerController.GetNowWeaponType() == WeaponType.WeaponB)
        {
            storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;

            if (Ammo > 0)
                Ammo = prevAmmoLeft - storeAmmoCost;
            else
            {
                Ammo = 0;
                prevAmmoLeft = 0;
            }
        }
        else if (playerController.GetNowWeaponType() == WeaponType.WeaponC)
        {
            storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;

            if (Ammo > 0)
                Ammo = prevAmmoLeft - storeAmmoCost;
            else
            {
                Ammo = 0;
                prevAmmoLeft = 0;
            }
        }
        else if (playerController.GetNowWeaponType() == WeaponType.WeaponD)
        {
            storeAmmoCost += buttonStayCostByTakeInMachine * Time.fixedDeltaTime * 60;

            if (Ammo > 0)
                Ammo = prevAmmoLeft - storeAmmoCost;
            else
            {
                Ammo = 0;
                prevAmmoLeft = 0;
            }
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
    public void AmmoRecoveryByBubble(float _bubbleSize)
    {
        Ammo += _bubbleSize * factorToCalAmmoRecovery;
        if (Ammo >= MaxAmmo)
            Ammo = MaxAmmo;
    }

    public void AmmoRecovertByGetItem()
    {
        Ammo = MaxAmmo;
        prevAmmoLeft = MaxAmmo;
    }
    public int GetNowAmmo()
    {
        return (int)Ammo;
    }
}
