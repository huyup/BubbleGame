using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionForItem : MonoBehaviour
{
    private ItemSetCtr itemSetCtr;
    // Use this for initialization
    void Start()
    {
        itemSetCtr = transform.GetComponentInParent<ItemSetCtr>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        
    }
    private void OnTriggerEnter(Collider _other)
    {
        Debug.Log(_other.transform.name);
        if (_other.gameObject.layer == 12/*EnemyHit*/||
            _other.gameObject.layer == 15/*EnemyAttack*/)
        {
            GetComponent<BubbleItemMovement>().IsHitBoss = true;
        }
        if (_other.gameObject.layer == 11/*PlayerTrigger*/)
        {
            if (_other.transform.GetComponent<PlayerController>())
            {
                itemSetCtr.GetItem(_other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 12/*EnemyHit*/||
            _other.gameObject.layer == 15/*EnemyAttack*/)
        {
            GetComponent<BubbleItemMovement>().IsHitBoss = false;
        }
    }
}
