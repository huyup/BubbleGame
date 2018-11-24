using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerWeaponBase : MonoBehaviour
{
    public virtual void OnAttackButtonDown()
    {
        GetComponent<PlayerAnimatorCtr>().SetAttackAnimationOnButtonDown();
    }

    public virtual void OnAttackButtonStay()
    {
        GetComponent<PlayerAnimatorCtr>().SetAttackAnimationOnButtonStay();
    }

    public virtual void OnAttackButtonUp()
    {
        GetComponent<PlayerAnimatorCtr>().SetAttackAnimationOnButtonUp();
    }
}


