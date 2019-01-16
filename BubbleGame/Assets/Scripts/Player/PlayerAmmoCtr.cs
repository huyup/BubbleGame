using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoCtr : MonoBehaviour
{
    public float NowAmmoLeft { get; set; }

    [SerializeField]
    public float MaxAmmo = 100;

    [SerializeField]
    private float factorToCalAmmoRecovery = 1.5f;


    
    // Use this for initialization
    void Start()
    {
        NowAmmoLeft = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("NowAmmoLeft" + NowAmmoLeft);
        if (NowAmmoLeft <= 0)
            NowAmmoLeft = 0;
    }

    public int GetNowAmmoLeft()
    {
        return (int)NowAmmoLeft;
    }
    public void AmmoRecovery(float _bubbleSize)
    {
        NowAmmoLeft += _bubbleSize * factorToCalAmmoRecovery;
        if (NowAmmoLeft >= MaxAmmo)
            NowAmmoLeft = MaxAmmo;
    }

    public void OnButtonDownCost(PlayerWeapon _weapon)
    {
        if (NowAmmoLeft < _weapon.GetMinShootCost())
            return;

        isAttacking = true;
        storeAmmoCost = minShootCost;
        playerAmmoCtr.NowAmmoLeft = prevAmmoLeft - storeAmmoCost;
    }

    public void OnButtonStayCost()
    {
        //弾薬計算
        if (storeAmmoCost < maxAmmoCost)
        {
            storeAmmoCost += buttonStayCost * Time.fixedDeltaTime * 60;
        }
        playerAmmoCtr.NowAmmoLeft = prevAmmoLeft - storeAmmoCost;
    }

    public void OnButtonUpCost()
    {

    }
}
