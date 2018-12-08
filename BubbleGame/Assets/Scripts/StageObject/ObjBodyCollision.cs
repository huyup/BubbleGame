using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXME:要らないかもしれない
/// </summary>
public class ObjBodyCollision : MonoBehaviour
{
    [SerializeField]
    private ObjController controller;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/)
        {
            controller.OnReset();
        }
    }
}
