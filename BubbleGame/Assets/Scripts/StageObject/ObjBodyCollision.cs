using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXME:要らないかもしれない
/// </summary>
public class ObjBodyCollision : MonoBehaviour
{
    private ObjFloatByContain floatByContain;

    private void Start()
    {
        floatByContain = GetComponent<ObjFloatByContain>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9/*Ground*/)
            floatByContain.ResetFloatFlag();
    }
}
