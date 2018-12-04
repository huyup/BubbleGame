using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTrigger : MonoBehaviour {
    
    private void OnTriggerEnter(Collider _other)
    {
        _other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
