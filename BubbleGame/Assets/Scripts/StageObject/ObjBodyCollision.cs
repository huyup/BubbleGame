using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXME:要らないかもしれない
/// </summary>
public class ObjBodyCollision : MonoBehaviour
{
    private ObjController controller;

    private void Start()
    {
        controller = GetComponent<ObjController>();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.layer == 9/*Ground*/)
            controller.ResetFloatFlag();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 9/*Ground*/)
            controller.ResetFloatFlag();
    }
}
