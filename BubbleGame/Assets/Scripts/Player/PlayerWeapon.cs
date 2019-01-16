using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.Serialization;

//マルチコントローラーアセット

public class PlayerWeapon : MonoBehaviour
{
    public bool CanAttack { get; private set; }

    public virtual bool GetIsAttacking()
    {
        return false;
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

    public virtual void OnChangeWeapon()
    {

    }
}


