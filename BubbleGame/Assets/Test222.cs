using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test222 : MonoBehaviour
{
    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("OnCollisionEnter" + _collision.gameObject.name);
    }
    private void OnTriggerEnter(Collider _other)
    {
        Debug.Log("OnTriggerEnter" + _other.name);
    }
}
