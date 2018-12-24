using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    [SerializeField]
    private List<Collider> insideColliderList = new List<Collider>();

    private BubbleSetController setController;
    private BubbleController controller;
    private bool canAddForceToInsideObj;

    private bool canAddInsideObj = true;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<BubbleController>();
        setController = transform.parent.GetComponent<BubbleSetController>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 11/*PlayerTrigger*/ && controller.GetBubbleState() != BubbleState.Creating)
        {
            setController.DestroyBubbleSet();
        }
        if (_other.gameObject.layer == 12 /*EnemyHit*/ || _other.gameObject.layer == 16 /*StageObj*/)
        {
            if (canAddInsideObj)
            {
                insideColliderList.Add(_other);
                canAddForceToInsideObj = true;
                canAddInsideObj = false;
            }
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 12 /*EnemyHit*/ || _other.gameObject.layer == 16 /*StageObj*/)
        {
            insideColliderList.Remove(_other);
        }
    }

    public void SetInsideObjDestroyable()
    {
        foreach (Collider insideCollider in insideColliderList)
        {
            if (insideCollider && insideCollider.GetComponent<ObjController>())
                insideCollider.GetComponent<ObjBodyCollision>().SetObjDestroy();

        }
    }
    public void AddForceToInsideObj(Vector3 _direction)
    {
        if (!canAddForceToInsideObj)
            return;

        foreach (Collider insideCollider in insideColliderList)
        {
            if (insideCollider && insideCollider.GetComponent<ObjController>())
                insideCollider.GetComponent<ObjController>().AddForceByPush(_direction);

        }
    }
}
