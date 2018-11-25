using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private BubbleProperty bubbleProperty;
    private BubbleCollision bubbleCollision;
    private Rigidbody rb;

    private bool canSetVelocity = true;
    
    // Use this for initialization
    void Start()
    {
        bubbleProperty = GetComponent<BubbleProperty>();
        bubbleCollision = GetComponent<BubbleCollision>();
        rb = GetComponent<Rigidbody>();
    }
    public void SetRigidbodyVelocityOnce(Vector3 _velocity)
    {
        if (canSetVelocity)
        {
            bubbleProperty.IsForceFloating = true;
            bubbleCollision.SetDestroyEnable();
            rb.velocity = Vector3.zero;
            rb.velocity = _velocity;
            canSetVelocity = false;
        }
    }
}
