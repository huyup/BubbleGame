using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatByContain : MonoBehaviour
{
    /// <summary>
    /// 上昇中かどうか
    /// </summary>
    private bool canFloat = false;
    
    /// <summary>
    ///　中心点に移動できるかどうか
    /// </summary>
    private bool canChangeVelocityToCenter = false;

    private Transform bubble;

    private Rigidbody rb;
    

    [SerializeField]
    private EnemyFunctionRef enemyFunctionRef;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
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
        enemyFunctionRef.GetEnemyStatus().SetIsFloating(true);
        canChangeVelocityToCenter = true;
        rb.velocity = Vector3.zero;
        GetComponent<CharacterController>().enabled = false;
    }
    private void MoveToCenterPos()
    {
        if (Vector3.Distance(transform.position, bubble.position) > 0.1f)
        {
            if (canChangeVelocityToCenter)
            {
                Vector3 direction = (bubble.position - transform.position).normalized;
                rb.velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubble.position) < 0.1f)
        {
            canChangeVelocityToCenter = false;
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
        enemyFunctionRef.GetEnemyStatus().SetIsFloating(false);
        if (!GetComponent<CharacterController>().enabled)
            GetComponent<CharacterController>().enabled = true;
    }
}
