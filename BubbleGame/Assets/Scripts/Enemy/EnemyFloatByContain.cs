using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO:今EnemyControllerの中に入っている機能をこちらへ移行する
/// </summary>
public class EnemyFloatByContain : MonoBehaviour
{
    /// <summary>
    /// 上昇中かどうか
    /// </summary>
    private bool canFloat = false;
    
    /// <summary>
    /// 速度を変えられるかどうか
    /// </summary>
    private bool canChangeVelocity = false;

    private Transform bubble;

    private Rigidbody rb;

    private EnemyController controller;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (canFloat)
        {
            if (bubble == null)
            {
                AddFallForce();
            }
            else
            {
                MoveToCenterPos();
            }
        }
    }
    public void FloatByContainOnInit(Transform _bubble)
    {
        this.bubble = _bubble;
        canFloat = true;
        controller.IsFloating = true;
        canChangeVelocity = true;
        rb.velocity = Vector3.zero;
        GetComponent<CharacterController>().enabled = false;
    }
    private void MoveToCenterPos()
    {
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

    private void AddFallForce()
    {
        rb.velocity = Physics.gravity;
    }
    public void Reset()
    {
        canFloat = false;
        rb.velocity = Vector3.zero;
        controller.IsFloating = false;
        if (!GetComponent<CharacterController>().enabled)
            GetComponent<CharacterController>().enabled = true;
    }
}
