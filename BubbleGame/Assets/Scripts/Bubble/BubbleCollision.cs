using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    private BubbleSetController setController;

    private bool canBeDestroy;

    // Use this for initialization
    void Start()
    {
        canBeDestroy = false;
        setController = transform.parent.GetComponent<BubbleSetController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && canBeDestroy)
        {
            setController.DestroyBubbleSet();
        }
        if (collision.gameObject.tag == "Ground" && canBeDestroy)
        {
            setController.DestroyBubbleSet();
        }
    }
    public void SetDestroyEnable()
    {
        canBeDestroy = true;
    }
}
