using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    private BubbleSetController setController;

    [SerializeField]
    private bool canBeDestroy;

    // Use this for initialization
    void Start()
    {
        canBeDestroy = false;
        setController = transform.parent.GetComponent<BubbleSetController>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 11/*PlayerTrigger*/ && canBeDestroy)
        {
            setController.DestroyBubbleSet();
        }
    }
    public void SetDestroyEnable()
    {
        canBeDestroy = true;
    }
}
