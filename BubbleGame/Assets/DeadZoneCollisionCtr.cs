using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneCollisionCtr : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        _other.GetComponent<ObjController>().Dead();
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<ObjController>().Dead();
    }
}
