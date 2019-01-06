using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerController controller;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.layer == 15/*EnemyHit*/&&!other.gameObject.CompareTag("SearchTrigger"))||
            (other.gameObject.layer==16/*Environment*/&&other.GetComponent<ObjController>().ObjState==ObjState.Falling))
        {
            controller.Damage();
        }
    }
}
