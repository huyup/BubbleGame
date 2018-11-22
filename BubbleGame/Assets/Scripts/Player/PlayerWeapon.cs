using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerWeapon : MonoBehaviour
{
    public virtual void OnAttackButtonDown()
    {
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonDown();
    }

    public virtual void OnAttackButtonStay()
    {
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonStay();
    }

    public virtual void OnAttackButtonUp()
    {
        GetComponent<PlayerAnimator>().SetAttackAnimationOnButtonUp();
    }
}


