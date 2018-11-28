using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    /// <summary>
    /// 速度を変えられるかどうか
    /// </summary>
    [SerializeField]
    bool canChangeVelocity = false;

    /// <summary>
    /// 落下中かどうか
    /// </summary>
    [SerializeField]
    public bool IsFalling = false;

    /// <summary>
    /// 上昇中できるかどうか
    /// </summary>
    [SerializeField]
    private bool canFloat = false;

    private Transform bubble;

    private float objInitMass;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        objInitMass = GetComponent<Rigidbody>().mass;
    }

    void Update()
    {
        if (canFloat)
            MoveToCenterPos();

        if (IsFalling)
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
            IsFalling = true;
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
        IsFalling = false;
        canFloat = false;
    }
}
