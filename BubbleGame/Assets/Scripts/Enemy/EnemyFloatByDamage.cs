using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatByDamage : MonoBehaviour
{
    [SerializeField]
    private Transform bubbleSetInstanceRef;

    [SerializeField]
    private Transform bubbleInstanceStartRef;

    [SerializeField]
    private float bubbleMaxSize;

    private Transform bubbleInstance;

    [SerializeField]
    private float increaseScaleVelocity;

    [SerializeField]
    private float factorToFloat;

    private Rigidbody rb;

    private EnemyController enemyController;

    private bool canSetInitPosToBubble = true;

    private bool canFloat = false;

    /// <summary>
    ///　中心点に移動できるかどうか
    /// </summary>
    private bool canChangeVelocityToCenter = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyController = GetComponent<EnemyController>();
    }
    private void Update()
    {
        if (enemyController.IsDied)
            return;
        if (canFloat)
        {
            if (bubbleInstance == null)
            {
                AddFallForce();
            }
            else
            {
                MoveToCenterPos();
            }
        }
    }
    public void CreateBubbleByDamage()
    {
        if (enemyController.IsDied)
            return;

        if (canSetInitPosToBubble)
        {
            CreateBubbleByDamageOnInit();
            canSetInitPosToBubble = false;
        }

        CreateBubbleByDamageOnUpdate();
    }

    private void CreateBubbleByDamageOnInit()
    {
        Transform bubbleSetInstance = Instantiate(bubbleSetInstanceRef) as Transform;

        bubbleInstance = bubbleSetInstance.transform.Find("Bubble");

        bubbleInstance.GetComponent<BubbleProperty>().IsCreatedByDamage = true;
        bubbleInstance.position = bubbleInstanceStartRef.position;
    }

    private void CreateBubbleByDamageOnUpdate()
    {
        if (!canFloat)
        {
            Vector3 scaleVelocity = new Vector3(increaseScaleVelocity, increaseScaleVelocity, increaseScaleVelocity) *
                                    Time.fixedDeltaTime * 60;

            Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60 * factorToFloat;

            if (bubbleInstance.localScale.x < bubbleMaxSize)
            {
                bubbleInstance.localScale += scaleVelocity;
                bubbleInstance.GetComponent<Rigidbody>().velocity = upVelocity;
            }
            else
            {
                bubbleInstance.GetComponent<BubbleController>().SetFloatVelocityToBubble();
                FloatByContainOnInit();
                canFloat = true;
            }
        }
    }

    private void MoveToCenterPos()
    {
        if (Vector3.Distance(transform.position, bubbleInstance.position) > 0.1f)
        {
            if (canChangeVelocityToCenter)
            {
                Vector3 direction = (bubbleInstance.position - transform.position).normalized;
                rb.velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubbleInstance.position) < 0.1f)
        {
            canChangeVelocityToCenter = false;
            rb.velocity = new Vector3(0, bubbleInstance.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }
    public void FloatByContainOnInit()
    {
        canFloat = true;
        GetComponent<EnemyController>().IsFloating = true;
        canChangeVelocityToCenter = true;
        rb.velocity = Vector3.zero;
        GetComponent<CharacterController>().enabled = false;
    }
    private void AddFallForce()
    {
        rb.velocity = Physics.gravity;
    }

    public void Reset()
    {
        //TODO:ここリセットする
        canFloat = false;
        rb.velocity = Vector3.zero;
        GetComponent<EnemyController>().IsFloating = false;
        if (!GetComponent<CharacterController>().enabled)
            GetComponent<CharacterController>().enabled = true;
    }
}
