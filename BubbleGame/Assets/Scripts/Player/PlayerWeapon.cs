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
    protected float ReloadSpeed;

    [SerializeField]
    public bool CanAttack { get; private set; }

    [SerializeField]
    protected int minShootCost;

    [SerializeField]
    protected float buttonStayCost;

    [SerializeField]
    protected int maxAmmoCost;

    public virtual int GetMinShootCost()
    {
        return minShootCost;
    }

    public virtual float GetButtonStayCost()
    {
        return buttonStayCost;
    }

    public virtual int GetMaxAmmoCost()
    {
        return maxAmmoCost;
    }

    public void BanAttack()
    {
        CanAttack = false;
    }

    public void ResetAttack()
    {
        CanAttack = true;
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

    public virtual void OnChange()
    {

    }
}


