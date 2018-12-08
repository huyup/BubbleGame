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

    private bool canAddForceByPush = true;

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

    public void AddForceByPush(Vector3 _direction)
    {
        if (canAddForceByPush)
        {
            rb.velocity += _direction;
            bubbleCollision.AddForceToInsideObj(_direction);
            canAddForceByPush = false;
            StartCoroutine(EarlyDestroyByAirGun());
        }
    }

    IEnumerator EarlyDestroyByAirGun()
    {
        if (!transform.parent)
            yield break;
        yield return new WaitForSeconds(bubbleProperty.LastTimeByAirGun);
        if (!transform.parent)
            yield break;
        transform.parent.GetComponent<BubbleSetController>().DestroyBubbleSet();
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
