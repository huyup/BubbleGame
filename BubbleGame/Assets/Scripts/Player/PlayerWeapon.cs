using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.Serialization;

//マルチコントローラーアセット

public class PlayerWeapon : MonoBehaviour
{
    [FormerlySerializedAs("weaponLeftCount")]

    [SerializeField]
    protected int MaxAmmo;

    [SerializeField]
    protected float ReloadSpeed;

    [SerializeField]
    public bool CanAttack { get; private set; }

    public void BanAttack()
    {
        CanAttack = false;
    }

    public void ResetAttack()
    {
        CanAttack = true;
    }

    public virtual int GetNowAmmo()
    {
        return MaxAmmo;
    }

    public virtual void OnAttackButtonDown()
    {

    }

    public virtual void OnAttackButtonStay()
    {

    }

    public virtual void OnAttackButtonUp()
    {

    }

    public virtual void OnReset()
    {

    }

    public virtual void AmmoRecovery(float _bubbleSize)
    {

    }
}


