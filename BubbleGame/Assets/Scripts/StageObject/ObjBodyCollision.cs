using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjBodyCollision : MonoBehaviour
{
    [SerializeField]
    private ObjController controller;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9 /*Ground*/&&controller.ObjState==ObjState.Falling)
        {
            controller.OnReset();
        }
    }
}
