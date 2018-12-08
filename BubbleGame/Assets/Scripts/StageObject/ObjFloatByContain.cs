using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjFloatByContain : MonoBehaviour
{
    [SerializeField]
    private bool canMoveToCenter = false;
    
    [SerializeField]
    private bool canStartFloating = false;

    private Transform bubble;

    private ObjController objController;

    private float objInitMass;

    private Rigidbody rb;
    
    private void Start()
    {
        objController = GetComponent<ObjController>();
        rb = GetComponent<Rigidbody>();
        objInitMass = GetComponent<Rigidbody>().mass;
    }

    void Update()
    {
        if (canStartFloating)
        {
            if (bubble == null)
            {
                rb.velocity = Physics.gravity * 1.2f;
                rb.mass = 1;
                objController.IsFalling = true;
                canStartFloating = false;
                return;
            }
            else
            {
                if (!objController.IsPushingByAirGun)
                    MoveToCenterPos();
            }
        }
    }

    public void SetFloatOnInit(Transform _bubble)
    {
        this.bubble = _bubble;
        canStartFloating = true;
        canMoveToCenter = true;
        rb.velocity = Vector3.zero;
    }
    private void MoveToCenterPos()
    {

        if (Vector3.Distance(transform.position, bubble.position) > 0.1f)
        {
            if (canMoveToCenter)
            {
                Vector3 direction = (bubble.position - transform.position).normalized;
                rb.velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubble.position) < 0.1f)
        {
            canMoveToCenter = false;
            rb.velocity = new Vector3(0, bubble.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }

    public void ResetFloatFlag()
    {
        rb.velocity = Vector3.zero;
        rb.mass = objInitMass;
        GetComponent<BoxCollider>().isTrigger = false;
        objController.IsFalling = false;
        canStartFloating = false;
    }
}
