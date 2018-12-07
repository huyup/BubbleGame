using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    private BubbleSetController setController;

    private BubbleController bubbleController;

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

    private void OnTriggerStay(Collider _other)
    {
        Debug.Log("Name"+_other.name);
    }

    public void SetDestroyEnable()
    {
        canBeDestroy = true;
    }
}
