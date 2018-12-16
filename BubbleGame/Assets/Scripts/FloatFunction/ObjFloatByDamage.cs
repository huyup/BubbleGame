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

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private ObjStatus status;
    private Transform bubbleInstance;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void CreateBubbleByDamage()
    {
        if (canCreateBubbleInstanceByDamage)
        {

            CreateBubbleByDamageOnInit();
            canCreateBubbleInstanceByDamage = false;
        }

        if (!bubbleInstance)
            return;

        if (status.Type != ObjType.Inoshishi)
        {
            //XXX:もし、泡のscaleがbubbleMaxSiezより大きい場合は、参照が見つからなくなる
            if (bubbleInstance.localScale.x < bubbleInstance.GetComponent<BubbleProperty>().MaxSizeCreatedByDamage)
            {
                SetBubbleToFloatPos();
            }
            else
            {
                bubbleInstance.GetComponent<BubbleController>().SetFloatVelocityToBubble();
                objFloatByContain.FloatByContain(bubbleInstance);
            }
        }
        else
        {
            //XXX:もし、泡のscaleがbubbleMaxSiezより大きい場合は、参照が見つからなくなる
            if (bubbleInstance.localScale.x < 14)
            {
                SetBubbleToFloatPos();
            }
            else
            {
                bubbleInstance.GetComponent<BubbleController>().SetFloatVelocityToBubble();
                objFloatByContain.FloatByContain(bubbleInstance);
            }

        }
    }

    private void CreateBubbleByDamageOnInit()
    {
        Transform bubbleSetInstance = Instantiate(bubbleSetInstanceRef) as Transform;

        bubbleInstance = bubbleSetInstance.transform.Find("Bubble");

        bubbleInstance.GetComponent<BubbleProperty>().IsCreatedByDamage = true;

        bubbleInstance.position = bubbleInstanceStartRef.position;

        if (status.Type == ObjType.Uribou)
        {
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
    }
    public void ResetFloatFlag()
    {
        rb.velocity = Vector3.zero;
        canCreateBubbleInstanceByDamage = true;
    }
}
