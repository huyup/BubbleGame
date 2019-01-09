using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BubbleState
{
    Creating,
    StandBy,
    Floating,
    BeTakeIn,
    BePressed,
}
public class BubbleController : MonoBehaviour
{
    private Vector3 tornadoPosition;

    private BubbleState nowBubbleState;

    private BubbleCollision bubbleCollision;
    private Rigidbody rb;

    [SerializeField]
    private float upForceWhenContain = 2;

    [SerializeField]
    private float lastTimeByAirGun = 1f;

    [SerializeField]
    private float timeToStopBubble = 1f;

    [SerializeField]
    private float takeInSpeedByTornado = 5;

    [SerializeField]
    private float stopDistanceByTornado = 1.5f;

    private bool canAddForceByPush = true;

    private bool canAddForceToBubble = true;

    private bool canStopBubble = true;


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
        else if (nowBubbleState == BubbleState.BeTakeIn)
            TakeInByTornado();
        else
            CancelInvoke("DelayStopBubble");
    }

    public void SetTornadoPosition(Vector3 _destination)
    {
        tornadoPosition = _destination;
    }

    public void TakeInByTornado()
    {
        var tornadoPositionInBubbleHeight =  new Vector3(tornadoPosition.x, transform.position.y, tornadoPosition.z);
        if (Vector3.Distance(tornadoPositionInBubbleHeight, transform.position) < stopDistanceByTornado)
        {
            nowBubbleState = BubbleState.StandBy;
            rb.velocity = Vector3.zero;
        }
        else
        {

            var direction = (tornadoPositionInBubbleHeight - transform.position).normalized;
            rb.velocity = direction * Time.fixedDeltaTime * 60 * takeInSpeedByTornado;
        }
    }

    private void DelayStopBubble()
    {
        if (canStopBubble)
        {
            rb.velocity = Vector3.zero;
            canStopBubble = false;
        }
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
        rb.velocity = _direction;
        if (bubbleCollision)
            bubbleCollision.AddForceToInsideObj(_direction);
        StartCoroutine(ResetDestroyTimeByAirGun());
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
