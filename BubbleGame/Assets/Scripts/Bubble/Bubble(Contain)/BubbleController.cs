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
    public PlayerSelection PlayerSelectionWhoCreated { get; set; }
    public PlayerSelection PlayerSelectionWhoPush { get; set; }
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

    [SerializeField]
    private float timeToDestroyBubble = 2;

    private bool canAddForceToBubble = true;

    private bool canStopBubble = true;

    [SerializeField]
    private float pingPongSpeed = 0.5f;

    [SerializeField]
    private float pingPongLength = 2;

    private bool canStartMove = true;
    // Use this for initialization
    void Start()
    {
        bubbleCollision = GetComponent<BubbleCollision>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log("BubbleState" + nowBubbleState);
        if (nowBubbleState == BubbleState.StandBy)
        {
            if (canStartMove)
            {
                StartCoroutine(BubbleMovement());
                canStartMove = false;
            }

            Invoke("DelayDestroyBubble", timeToStopBubble + timeToDestroyBubble);
        }
        else
        {
            //CancelInvoke("DelayStopBubble");
            CancelInvoke("DelayDestroyBubble");
        }
        if (nowBubbleState == BubbleState.BeTakeIn)
        {
            TakeInByTornado();
        }
    }

    IEnumerator BubbleMovement()
    {
        Debug.Log("up");
        //rb.AddForce(Vector3.left * 0.2f, ForceMode.VelocityChange);
        rb.AddForce(Vector3.up, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("down");
        //rb.AddForce(Vector3.right * 0.5f, ForceMode.VelocityChange);
        rb.AddForce(Vector3.down * 2, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1.5f);
        //rb.AddForce(Vector3.left * 0.3f, ForceMode.VelocityChange);
        rb.AddForce(Vector3.up, ForceMode.VelocityChange);
    }
    public void SetTornadoPosition(Vector3 _destination)
    {
        tornadoPosition = _destination;
    }

    public void TakeInByTornado()
    {
        var tornadoPositionInBubbleHeight = new Vector3(tornadoPosition.x, transform.position.y, tornadoPosition.z);
        if (Vector3.Distance(tornadoPositionInBubbleHeight, transform.position) < stopDistanceByTornado)
        {
            nowBubbleState = BubbleState.StandBy;
            rb.velocity = Vector3.zero;
        }
        else
        {

            var direction = (tornadoPositionInBubbleHeight - transform.position).normalized;
            rb.velocity = direction * Time.fixedDeltaTime * 60 * takeInSpeedByTornado;
            if (bubbleCollision)
                bubbleCollision.TakeObjInByTornado(tornadoPosition, takeInSpeedByTornado, stopDistanceByTornado);
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

    private void DelayDestroyBubble()
    {
        GetComponentInParent<BubbleSetController>().DestroyBubbleSet();
    }
    public BubbleState GetBubbleState()
    {
        return nowBubbleState;
    }
    public void SetBubbleState(BubbleState _nextBubbleState)
    {
        nowBubbleState = _nextBubbleState;
    }

    public void AddForceByPush(Vector3 _direction, PlayerSelection _playerSelection)
    {
        PlayerSelectionWhoPush = _playerSelection;
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
    public void SetFloatVelocityToBubble(float _bubbleForwardPower, float _bubbleUpPower)
    {
        if (canAddForceToBubble)
        {
            rb.velocity = Vector3.zero;
            nowBubbleState = BubbleState.StandBy;
            rb.AddForce(transform.forward * _bubbleForwardPower);
            rb.AddForce(transform.up * _bubbleUpPower);
            canAddForceToBubble = false;
        }
    }
}
