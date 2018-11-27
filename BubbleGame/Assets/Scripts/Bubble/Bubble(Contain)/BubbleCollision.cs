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
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.layer==16 && canBeDestroy)
        {
            setController.DestroyBubbleSet();
        }
        if (_collision.gameObject.tag == "Ground" && canBeDestroy)
        {
            setController.DestroyBubbleSet();
        }
    }
    public void SetDestroyEnable()
    {
        canBeDestroy = true;
    }
}
