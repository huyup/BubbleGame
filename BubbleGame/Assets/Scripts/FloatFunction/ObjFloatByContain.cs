﻿using UnityEngine;
using UnityEngine.AI;

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

    private float initAngularDrag;
    private float initDrag;

    private bool isCreatedByDamage = false;

    private void Start()
    {

        objController = GetComponent<ObjController>();
        rb = GetComponent<Rigidbody>();
        initAngularDrag = rb.angularDrag;
        initDrag = rb.drag;
    }

    public void SetCreatedByDamageFlag(bool _flag)
    {
        isCreatedByDamage = _flag;
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
        rb.isKinematic = false;
        if (status.Type == ObjType.Uribou || status.Type == ObjType.Harinezemi || status.Type == ObjType.Inoshishi)
        {
            agent.enabled = false;

            GetComponent<Animator>().applyRootMotion = false;
        }
        rb.angularDrag = 0.05f;
        rb.drag = 0.05f;
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
        if (isCreatedByDamage)
            rb.velocity = new Vector3(0, bubbleInstance.GetComponent<Rigidbody>().velocity.y, 0);
        else
            rb.velocity = Vector3.zero;
    }
    public void ResetFloatFlag()
    {
        rb.angularDrag = initAngularDrag;
        rb.drag = initDrag;
        canMoveToCenter = false;
        rb.velocity = Vector3.zero;
        canStartFloating = false;
        isCreatedByDamage = false;
    }
}
