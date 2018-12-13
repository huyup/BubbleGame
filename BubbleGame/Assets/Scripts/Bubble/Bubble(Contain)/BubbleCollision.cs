using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    private BubbleSetController setController;

    [SerializeField]
    private bool canBeDestroy;

    [SerializeField]
    private bool canAddForceToInsideObj;

    [SerializeField]
    private List<Collider> insideColliderList = new List<Collider>();
    
    // Use this for initialization
    void Start()
    {
        canBeDestroy = false;
        setController = transform.parent.GetComponent<BubbleSetController>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 11/*PlayerTrigger*/ && canBeDestroy)
        {
            setController.DestroyBubbleSet();
        }
        if (_other.gameObject.layer == 12 /*Uribou*/ || _other.gameObject.layer == 16 /*StageObj*/)
        {
            insideColliderList.Add(_other);
        }
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.layer == 12 /*Uribou*/ || _other.gameObject.layer == 16 /*StageObj*/)
        {
            canAddForceToInsideObj = true;
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 12 /*Uribou*/ || _other.gameObject.layer == 16 /*StageObj*/)
        {
            insideColliderList.Remove(_other);
        }
    }
    public void AddForceToInsideObj(Vector3 _direction)
    {
        if (!canAddForceToInsideObj)
            return;

        foreach (Collider insideCollider in insideColliderList)
        {
            if (insideCollider.gameObject.layer == 16/*StageObj*/)
                insideCollider.GetComponent<ObjController>().AddForceByPush(_direction);
            else if (insideCollider.gameObject.layer == 12/*Uribou*/)
                insideCollider.transform.parent.GetComponent<EnemyFloatByDamage>().AddForceByPush(_direction);
        }
    }


    public void SetDestroyEnable()
    {
        canBeDestroy = true;
    }
}
