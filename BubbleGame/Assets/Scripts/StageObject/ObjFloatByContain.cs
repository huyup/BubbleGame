using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFloatByContain : MonoBehaviour
{
    /// <summary>
    /// 速度を変えられるかどうか
    /// </summary>
    [SerializeField]
    bool canChangeVelocity = false;

    /// <summary>
    /// 上昇中できるかどうか
    /// </summary>
    [SerializeField]
    private bool canFloat = false;

    private Transform bubble;

    private float objInitMass;

    private Rigidbody rb;
    private ObjController objController;
    private void Start()
    {
        objController = GetComponent<ObjController>();
        rb = GetComponent<Rigidbody>();
        objInitMass = GetComponent<Rigidbody>().mass;
    }

    void Update()
    {
        if (canFloat)
            MoveToCenterPos();

        if (objController.IsFalling)
            GetComponent<BoxCollider>().isTrigger = true;
    }

    public void SetFloatOnInit(Transform _bubble)
    {
        this.bubble = _bubble;
        canFloat = true;
        canChangeVelocity = true;
        rb.velocity = Vector3.zero;
    }
    private void MoveToCenterPos()
    {
        if (bubble == null)
        {
            rb.velocity = Physics.gravity * 1.2f;
            rb.mass = 1;
            objController.IsFalling = true;
            canFloat = false;
            return;
        }
        if (Vector3.Distance(transform.position, bubble.position) > 0.1f)
        {
            if (canChangeVelocity)
            {
                Vector3 direction = (bubble.position - transform.position).normalized;
                rb.velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubble.position) < 0.1f)
        {
            canChangeVelocity = false;
            rb.velocity = new Vector3(0, bubble.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }

    public void ResetFloatFlag()
    {
        rb.velocity = Vector3.zero;
        rb.mass = objInitMass;
        GetComponent<BoxCollider>().isTrigger = false;
        objController.IsFalling = false;
        canFloat = false;
    }
}
