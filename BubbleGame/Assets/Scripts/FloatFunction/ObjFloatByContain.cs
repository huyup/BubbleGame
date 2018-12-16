using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ObjFloatByContain : MonoBehaviour
{
    [SerializeField]
    private bool canMoveToCenter = false;

    [SerializeField]
    private bool canStartFloating = false;

    [SerializeField]
    private float moveToCenterSpeed = 4;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private ObjStatus status;

    private Transform bubbleInstance;

    private ObjController objController;

    private Rigidbody rb;

    private void Start()
    {
        objController = GetComponent<ObjController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canStartFloating)
        {
            if (bubbleInstance == null)
            {
                Fallen();
            }
            else
            {
                if (objController.ObjState == ObjState.Floating)
                    FloatByContainOnUpdate();
            }
        }
    }
    public void FloatByContain(Transform _bubble)
    {
        this.bubbleInstance = _bubble;
        if (status.Type == ObjType.Uribou || status.Type == ObjType.Harinezemi || status.Type == ObjType.Inoshishi)
        {
            agent.enabled = false;
            rb.isKinematic = false;
            GetComponent<Animator>().applyRootMotion = false;
        }

        canStartFloating = true;
        canMoveToCenter = true;
        rb.velocity = Vector3.zero;
        objController.SetObjState(ObjState.Floating);
    }

    private void Fallen()
    {
        rb.velocity = Physics.gravity;
        objController.SetObjState(ObjState.Falling);
        canStartFloating = false;
    }
    private void FloatByContainOnUpdate()
    {
        if (status.Type != ObjType.Inoshishi)
        {
            if (Vector3.Distance(transform.position, bubbleInstance.position) > 0.1f)
            {
                if (canMoveToCenter)
                    MoveToCenter();
            }
            else if (Vector3.Distance(transform.position, bubbleInstance.position) < 0.1f)
            {
                canMoveToCenter = false;
                FloatByBubbleVelocity();
            }
        }
        else
        {
            FloatByBubbleVelocity();
        }

    }

    private void MoveToCenter()
    {
        Vector3 direction = (bubbleInstance.position - transform.position).normalized;
        rb.velocity = (direction * Time.fixedDeltaTime * 60 * moveToCenterSpeed);
    }

    private void FloatByBubbleVelocity()
    {
        rb.velocity = new Vector3(0, bubbleInstance.GetComponent<Rigidbody>().velocity.y, 0);
    }
    public void ResetFloatFlag()
    {
        rb.velocity = Vector3.zero;
        canStartFloating = false;
    }
}
