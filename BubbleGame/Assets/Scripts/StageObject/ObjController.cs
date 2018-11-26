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

    void Update()
    {
        if(canFloat)
            MoveToCenterPos();
    }

    public void SetCenterPos(Transform _bubble)
    {
        this.bubble = _bubble;
        canFloat = true;
        canChangeVelocity = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void MoveToCenterPos()
    {
        if (bubble == null)
        {
            GetComponent<Rigidbody>().velocity = Physics.gravity;
            IsFalling = true;
            canFloat = false;
            return;
        }
        if (Vector3.Distance(transform.position, bubble.position) > 0.1f)
        {
            if (canChangeVelocity)
            {
                Vector3 direction = (bubble.position - transform.position).normalized;
                GetComponent<Rigidbody>().velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubble.position) < 0.1f)
        {
            canChangeVelocity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, bubble.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }

    public void ResetFloatFlag()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        IsFalling = false;
        canFloat = false;
    }
}
