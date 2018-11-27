using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private BubbleProperty bubbleProperty;
    private BubbleCollision bubbleCollision;
    private Rigidbody rb;

    private bool canSetVelocity = true;

    [SerializeField]
    private float countToFloat = 2;

    [SerializeField]
    private float upForceToFloat=1.5f;

    private bool canAddAutoFloatForce = true;
    // Use this for initialization
    void Start()
    {
        bubbleProperty = GetComponent<BubbleProperty>();
        bubbleCollision = GetComponent<BubbleCollision>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!bubbleProperty.IsForceFloating)
        {
            Invoke("FloatByTime", countToFloat);
        }
        else
        {
            CancelInvoke("FloatByTime");
        }
    }

    private void FloatByTime()
    {
        if (!bubbleProperty.IsForceFloating&&canAddAutoFloatForce)
        {
            Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60*upForceToFloat;
            rb.velocity += upVelocity;
            canAddAutoFloatForce = false;
        }
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
