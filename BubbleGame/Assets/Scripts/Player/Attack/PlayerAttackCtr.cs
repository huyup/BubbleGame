using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    WeaponA = 1,
    WeaponB,
    WeaponC,
    Max,
}
public class PlayerAttackCtr : PlayerController {

    private WeaponType nowWeaponType;

    protected PlayerWeaponBase nowWeaponBase;

    [SerializeField]
    protected PlayerWeaponA weaponA;

    [SerializeField]
    protected PlayerWeaponB weaponB;

    public override void OnInitialize()
    {
        nowWeaponType = WeaponType.WeaponA;
        nowWeaponBase = null;
    }
    public void ChangeWeapon()
    {
        int nextWeaponTypeNum = (int)nowWeaponType;

        nextWeaponTypeNum++;

        if (nextWeaponTypeNum == (int)WeaponType.Max)
            nextWeaponTypeNum = (int)WeaponType.WeaponA;

        WeaponType nextWeaponType = (WeaponType)Enum.ToObject(typeof(WeaponType), nextWeaponTypeNum);

        nowWeaponType = nextWeaponType;
    }

    public PlayerWeaponBase GetWeapon()
    {
        switch (nowWeaponType)
        {
            case WeaponType.WeaponA:
                nowWeaponBase = weaponA;
                break;
            case WeaponType.WeaponB:
                nowWeaponBase = weaponB;
                break;
            case WeaponType.WeaponC:
                nowWeaponBase = null;
                break;
            default:
                nowWeaponBase = null;
                break;
        }

        if (nowWeaponBase == null)
        {
            Debug.Log("ErrorToSetWeapon");
        }

        return nowWeaponBase;
    }
}
