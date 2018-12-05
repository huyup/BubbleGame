using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXME:要らないかもしれない
/// </summary>
public class ObjBodyCollision : MonoBehaviour
{
    [SerializeField]
    private ObjFloatByContain floatByContain;

    [SerializeField]
    private ObjFloatByDamage floatByDamage;
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {
            floatByContain.ResetFloatFlag();
            floatByDamage.ResetFloatFlag();
        }
    }
}
