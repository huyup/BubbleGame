using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFloatByDamage : MonoBehaviour
{

    [SerializeField]
    private Transform bubbleSetInstanceRef;

    [SerializeField]
    private Transform bubbleInstanceStartRef;

    [SerializeField]
    private float bubbleMaxSize;

    private Transform bubbleInstance;

    private ObjController objController;

    [SerializeField]
    private float increaseScaleVelocity;

    [SerializeField]
    private float factorToFloat;

    private Rigidbody rb;

    [SerializeField]
    private bool canSetInitPosToBubble = true;

    [SerializeField]
    private bool canFloat = false;

    /// <summary>
    ///　中心点に移動できるかどうか
    /// </summary>
    [SerializeField]
    private bool canChangeVelocityToCenter = false;

    [SerializeField]
    private bool isPushing = false;
    void Start()
    {
        objController = GetComponent<ObjController>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (canFloat)
        {
            if (bubbleInstance == null)
            {
                AddFallForce();
            }
            else
            {
                if (!objController.IsPushingByAirGun)
                    MoveToCenterPos();
            }
        }
    }
    public void CreateBubbleByDamage()
    {

        if (canSetInitPosToBubble)
        {
            CreateBubbleByDamageOnInit();
            canSetInitPosToBubble = false;
        }

        CreateBubbleByDamageOnUpdate();
    }

    private void CreateBubbleByDamageOnInit()
    {
        Transform bubbleSetInstance = Instantiate(bubbleSetInstanceRef) as Transform;

        bubbleInstance = bubbleSetInstance.transform.Find("Bubble");

        bubbleInstance.GetComponent<BubbleProperty>().IsCreatedByDamage = true;
        bubbleInstance.position = bubbleInstanceStartRef.position;
    }
    private void CreateBubbleByDamageOnUpdate()
    {
        if (!bubbleInstance)
            return;
        if (!canFloat)
        {
            Vector3 scaleVelocity = new Vector3(increaseScaleVelocity, increaseScaleVelocity, increaseScaleVelocity) *
                                    Time.fixedDeltaTime * 60;

            Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60 * factorToFloat;

            //XXX:もし、泡のscaleがbubbleMaxSiezより大きい場合は、参照が見つからなくなる
            if (bubbleInstance.localScale.x < bubbleMaxSize)
            {
                bubbleInstance.localScale += scaleVelocity;
                bubbleInstance.GetComponent<Rigidbody>().velocity = upVelocity;
            }
            else
            {
                bubbleInstance.GetComponent<BubbleController>().SetFloatVelocityToBubble();
                FloatByContainOnInit();
                canFloat = true;
            }
        }
    }

    private void MoveToCenterPos()
    {
        if (Vector3.Distance(transform.position, bubbleInstance.position) > 0.1f)
        {
            if (canChangeVelocityToCenter)
            {
                Vector3 direction = (bubbleInstance.position - transform.position).normalized;
                rb.velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubbleInstance.position) < 0.1f)
        {
            canChangeVelocityToCenter = false;
            rb.velocity = new Vector3(0, bubbleInstance.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }
    private void FloatByContainOnInit()
    {
        canFloat = true;
        canChangeVelocityToCenter = true;
        rb.velocity = Vector3.zero;
    }
    private void AddFallForce()
    {
        objController.IsFalling = true;
        rb.velocity = Physics.gravity;
    }

    public void ResetFloatFlag()
    {
        //TODO:ここリセットする
        objController.IsFalling = false;
        rb.velocity = Vector3.zero;
        isPushing = false;
        canFloat = false;
        canSetInitPosToBubble = true;
        canChangeVelocityToCenter = false;
    }
}
