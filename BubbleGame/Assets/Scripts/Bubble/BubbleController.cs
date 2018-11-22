using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    BubbleProperty property;
    BubbleCollision collision;
    bool canSetVelocity = true;
    bool canChangeVelocity = true;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        collision = GetComponent<BubbleCollision>();
        property = GetComponent<BubbleProperty>();
        rb = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    if (rb.velocity.magnitude != 0&& !property.IsFloating)
    //        Invoke("ChangeBubbleVelocity", 1.2f);
    //    if(property.IsFloating)
    //        CancelInvoke("ChangeBubbleVelocity");
    //}
    //public void ChangeBubbleVelocity()
    //{
    //    if (canChangeVelocity)
    //    {
    //        rb.velocity += new Vector3(0, 1f, 0);
    //        canChangeVelocity = false;
    //    }
    //}

    public void SetRigibodyVelocityOnce(Vector3 _velocity)
    {
        if (canSetVelocity)
        {
            collision.SetDestroyFlag();
            GetComponent<BubbleProperty>().IsFloating = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = _velocity;
            canSetVelocity = false;
        }
    }
}
