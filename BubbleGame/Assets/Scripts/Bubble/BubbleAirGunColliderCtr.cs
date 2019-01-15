using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAirGunColliderCtr : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("BubbleAirCollider")&& collision.transform.root.GetComponent<BubbleSetController>())
        {
            collision.transform.root.GetComponent<BubbleSetController>().DestroyBubbleSet();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("BubbleAirCollider"))
        {
            collision.transform.root.GetComponent<BubbleSetController>().DestroyBubbleSet();
        }
    }
}
