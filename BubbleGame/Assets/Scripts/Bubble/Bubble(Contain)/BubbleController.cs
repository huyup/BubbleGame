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
    private float upForceToFloat = 1.5f;

    [SerializeField]
    private float upForceWhenContain;

    private bool canAddAutoFloatForce = true;

    public bool CanAddForceToInsideObj { get; private set; }

    public Vector3 AddForceToInsideObjDirection { get; private set; }
    // Use this for initialization
    void Start()
    {
        bubbleProperty = GetComponent<BubbleProperty>();
        bubbleCollision = GetComponent<BubbleCollision>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!bubbleProperty.IsForceFloating && !bubbleProperty.IsCreatedByDamage)
        {
            Invoke("FloatByTime", countToFloat);
        }
        else
        {
            CancelInvoke("FloatByTime");
        }
    }

    public void AddForce(Vector3 _direction)
    {
        rb.velocity += _direction;
        CanAddForceToInsideObj = true;
        AddForceToInsideObjDirection = _direction;
    }
    private void FloatByTime()
    {
        if (!bubbleProperty.IsForceFloating && canAddAutoFloatForce)
        {
            Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60 * upForceToFloat;
            rb.velocity += upVelocity;
            canAddAutoFloatForce = false;
        }
    }
    public void SetFloatVelocityToBubble()
    {
        if (canSetVelocity)
        {
            bubbleProperty.IsForceFloating = true;
            bubbleCollision.SetDestroyEnable();
            rb.velocity = Vector3.zero;
            rb.velocity = Vector3.up * upForceWhenContain;
            canSetVelocity = false;
        }
    }
}
