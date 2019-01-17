using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjFloatByContain : MonoBehaviour
{
    [SerializeField]
    private bool canMoveToCenter = false;

    [SerializeField]
    private bool canStartFloating = false;

    [SerializeField]
    private float moveToCenterSpeed = 4;

    private BehaviorsCtr behaviorCtr;

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

    private Vector3 boxColliderInitScale;

    private Vector3 defaultBoxColliderSize;
    private void Start()
    {
        behaviorCtr = GetComponent<BehaviorsCtr>();
        objController = GetComponent<ObjController>();
        rb = GetComponent<Rigidbody>();
        initAngularDrag = rb.angularDrag;
        initDrag = rb.drag;

        if (GetComponent<BoxCollider>())
            boxColliderInitScale = GetComponent<BoxCollider>().size;
    }
    
    void Update()
    {
        if (canStartFloating)
        {
            if (bubbleInstance == null)
            {
                Debug.Log("Fallen");
                Fallen();
            }
            else
            {
                Debug.Log("Float");
                if (objController.ObjState == ObjState.Floating)
                {
                    Debug.Log("Float");
                    FloatByContainOnUpdate();
                }
            }
        }
    }
    public void FloatByContain(Transform _bubble)
    {
        objController.SetObjState(ObjState.Floating);
        this.bubbleInstance = _bubble;
        rb.isKinematic = false;
        if (status.Type == ObjType.Uribou || status.Type == ObjType.Harinezemi || status.Type == ObjType.Inoshishi)
        {
            agent.enabled = false;
            behaviorCtr.DisableBehaviors();
            GetComponent<Animator>().applyRootMotion = false;
        }
        //if (GetComponent<BoxCollider>())
        //{
        //    GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x * 0.5f, 0,
        //        GetComponent<BoxCollider>().size.z * 0.5f);
        //}

        StartCoroutine(DelayResetBoxCollider());
        rb.useGravity = false;
        //rb.angularDrag = 0.05f;
        //rb.drag = 0.05f;
        canStartFloating = true;
        canMoveToCenter = true;
        //rb.velocity = Vector3.zero;
    }

    IEnumerator DelayResetBoxCollider()
    {
        yield return new WaitForSeconds(1.5f);
        if (GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().size = boxColliderInitScale;
        }
    }


    private void Fallen()
    {
        if (objController.ObjState == ObjState.Dead)
            return;

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
        rb.velocity = bubbleInstance.GetComponent<Rigidbody>().velocity;
    }
    public void ResetFloatFlag()
    {
        if (GetComponent<BoxCollider>())
            GetComponent<BoxCollider>().size = boxColliderInitScale;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.angularDrag = initAngularDrag;
        rb.drag = initDrag;
        canMoveToCenter = false;
        rb.velocity = Vector3.zero;
        canStartFloating = false;
        isCreatedByDamage = false;
    }
}
