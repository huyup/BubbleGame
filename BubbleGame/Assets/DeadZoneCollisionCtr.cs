using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneCollisionCtr : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.GetComponent<ObjController>())
            _other.GetComponent<ObjController>().Dead();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<ObjController>())
            collision.gameObject.GetComponent<ObjController>().Dead();
    }
}
