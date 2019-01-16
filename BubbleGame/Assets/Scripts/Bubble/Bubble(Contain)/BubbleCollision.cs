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
    private bool canAddStaminaDamageToBoss = true;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<BubbleController>();
        setController = transform.parent.GetComponent<BubbleSetController>();
    }


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 11/*PlayerTrigger*/
            && controller.GetBubbleState() != BubbleState.Creating
            && controller.GetBubbleState() != BubbleState.BePressed
            && !_other.gameObject.CompareTag("TornadoTrigger"))
        {
            if (_other.gameObject.GetComponent<PlayerStatus>().WeaponSelection != WeaponSelection.AirGun)
                _other.gameObject.GetComponent<PlayerAmmoCtr>().AmmoRecovery(this.transform.localScale.magnitude);
            setController.DestroyBubbleSet();
        }
        if (_other.gameObject.layer == 12 /*EnemyHit*/ || _other.gameObject.layer == 15 /*EnemyAttack*/)
        {
            if (_other.transform.root.GetComponent<ObjStatus>().Type == ObjType.Inoshishi)
            {
                if (controller.GetBubbleState() != BubbleState.Creating && canAddStaminaDamageToBoss)
                {
                    _other.transform.root.GetComponent<BossStaminaCtr>()
                        .StaminaDamageByBigBubble(transform.localScale.magnitude * 2.5f);
                    _other.transform.root.GetComponent<BossHateValueCtr>()
                        .IncreaseHateValueByBigBubble(transform.localScale.magnitude * 2.5f,
                            controller.PlayerSelectionWhoCreated);
                    StartCoroutine(DelayDestroy());
                    canAddStaminaDamageToBoss = false;
                }
            }
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

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        setController.DestroyBubbleSet();
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 12 /*EnemyHit*/ || _other.gameObject.layer == 16 /*StageObj*/)
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
            if (insideCollider && insideCollider.GetComponent<ObjController>())
                insideCollider.GetComponent<ObjController>().AddForceByPush(_direction, controller.PlayerSelectionWhoPush);

        }
    }

    public void TakeObjInByTornado(Vector3 _destinatio, float _takeInSpeed, float _stopDistance)
    {
        foreach (Collider insideCollider in insideColliderList)
        {
            if (insideCollider && insideCollider.GetComponent<ObjController>())
            {
                insideCollider.GetComponent<ObjController>()
                    .SetTakeInParameter(_destinatio, _takeInSpeed, _stopDistance);
                insideCollider.GetComponent<ObjController>().SetObjState(ObjState.MovingByTornado);

            }
        }
    }
}
