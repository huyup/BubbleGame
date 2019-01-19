using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjFloatByDamage : MonoBehaviour
{
    [SerializeField]
    private Transform bubbleSetInstanceRef;

    [SerializeField]
    private Transform bubbleInstanceStartRef;

    [SerializeField]
    private ObjFloatByContain objFloatByContain;

    [SerializeField]
    private float increaseScaleVelocity;

    [SerializeField]
    private float factorToFloatInCreating;

    [SerializeField]
    private bool canCreateBubbleInstanceByDamage = true;

    private Transform bubbleInstance;

    [SerializeField]
    private float maxSize = 14;

    [SerializeField]
    private BehaviorsCtr behaviorCtr;

    private Rigidbody rb;

    private ObjStatus status;

    private NavMeshAgent agent;
    void Start()
    {
        if (GetComponent<BehaviorsCtr>())
            behaviorCtr = GetComponent<BehaviorsCtr>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<ObjStatus>();
    }


    public void CreateBubbleByDamage()
    {
        //if (GetComponent<ObjController>().ObjState == ObjState.Floating)
        //    return;

        if (canCreateBubbleInstanceByDamage)
        {
            CreateBubbleByDamageOnInit();
            canCreateBubbleInstanceByDamage = false;
            //GetComponent<ObjController>().SetObjState(ObjState.Floating);
        }

        if (!bubbleInstance)
            return;

        //XXX:もし、泡のscaleがbubbleMaxSiezより大きい場合は、参照が見つからなくなる
        if (bubbleInstance.localScale.magnitude < maxSize)
        {
            SetBubbleToFloatPos();
        }
        else
        {
            bubbleInstance.GetComponent<BubbleController>().SetFloatVelocityToBubble(30, 30);
            //if (GetComponent<ObjController>().ObjState == ObjState.OnGround)
            //{
            Debug.Log("FloatByContain");
            objFloatByContain.FloatByContain(bubbleInstance);
            //}
        }
    }

    private void CreateBubbleByDamageOnInit()
    {
        Transform bubbleSetInstance = Instantiate(bubbleSetInstanceRef) as Transform;

        bubbleInstance = bubbleSetInstance.transform.Find("Bubble");

        bubbleInstance.position = bubbleInstanceStartRef.position;

        if (status.Type == ObjType.Uribou || status.Type == ObjType.Harinezemi || status.Type == ObjType.Inoshishi)
        {
            behaviorCtr.DisableBehaviors();
            agent.enabled = false;
        }
    }
    private void SetBubbleToFloatPos()
    {
        if (!bubbleInstance)
            return;

        Vector3 scaleVelocity = new Vector3(increaseScaleVelocity, increaseScaleVelocity, increaseScaleVelocity) *
                                Time.fixedDeltaTime * 60;

        Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60 * factorToFloatInCreating;

        bubbleInstance.localScale += scaleVelocity;
        bubbleInstance.GetComponent<Rigidbody>().velocity = upVelocity;

        //rb.isKinematic = false;
        //rb.velocity = bubbleInstance.GetComponent<Rigidbody>().velocity;
    }
    public void ResetFloatFlag()
    {
        rb.velocity = Vector3.zero;
        canCreateBubbleInstanceByDamage = true;
    }
}
