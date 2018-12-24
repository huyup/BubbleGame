using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionForItem : MonoBehaviour {
    private BubbleSetController setController;
    // Use this for initialization
    void Start()
    {
        setController = transform.parent.GetComponent<BubbleSetController>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 11/*PlayerTrigger*/)
        {
            _other.GetComponent<PlayerController>().UseAirGun();
            setController.DestroyBubbleItemSet();
        }
    }
}
