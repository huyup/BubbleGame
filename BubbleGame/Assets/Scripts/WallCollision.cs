using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{       
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 10 /*Bubble*/)
        {
            _other.GetComponent<BubbleCollision>().SetInsideObjDestroyable();
        }
    }
}
