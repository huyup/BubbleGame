using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
public enum BubbleState
{
    Creating,
    StandBy,
    Floating,
    BePressed,
}
public class BubbleController : MonoBehaviour
{
    private BubbleState nowBubbleState;

    private BubbleCollision bubbleCollision;
    private Rigidbody rb;

    [SerializeField]
    private float upForceWhenContain = 2;

    [SerializeField]
    private float lastTimeByAirGun = 1f;

    [SerializeField]
    private float timeToStopBubble = 1f;

    private bool canAddForceByPush = true;

    private bool canAddForceToBubble = true;

    // Use this for initialization
    void Start()
    {
        bubbleCollision = GetComponent<BubbleCollision>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (nowBubbleState == BubbleState.StandBy)
            Invoke("DelayStopBubble", timeToStopBubble);
        else
            CancelInvoke("DelayStopBubble");
    }

    private void DelayStopBubble()
    {
        rb.velocity = Vector3.zero;
    }
    public BubbleState GetBubbleState()
    {
        return nowBubbleState;
    }
    public void SetBubbleState(BubbleState _nextBubbleState)
    {
        nowBubbleState = _nextBubbleState;
    }

    public void AddForceByPush(Vector3 _direction)
    {
        if (canAddForceByPush)
        {
            Debug.Log("AddForce");
            rb.velocity += _direction;
            bubbleCollision.AddForceToInsideObj(_direction);
            canAddForceByPush = false;
            StartCoroutine(ResetDestroyTimeByAirGun());
        }
    }

    IEnumerator ResetDestroyTimeByAirGun()
    {
        if (!transform.parent)
            yield break;
        yield return new WaitForSeconds(lastTimeByAirGun);
        if (!transform.parent)
            yield break;
        transform.parent.GetComponent<BubbleSetController>().DestroyBubbleSet();
    }
    public void SetFloatVelocityToBubble()
    {
        if (canAddForceToBubble)
        {
            nowBubbleState = BubbleState.Floating;
            rb.velocity = Vector3.zero;
            rb.velocity = Vector3.up * upForceWhenContain;
            canAddForceToBubble = false;
        }
    }
}
