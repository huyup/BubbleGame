using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjJudgeCollision : MonoBehaviour
{
    GameObject calculationController;
    CalculateProperty calculationProperty;

    CalculateBoxContainBox judgeContainFunc;
    CalculateBiggerBoxCollider calculateBiggerFunc;

    ObjController controller;

    [SerializeField]
    private bool canBeContained = false;
    
    // Use this for initialization
    void Start()
    {
        calculationController = GameObject.Find("CalculationController");

        calculationProperty = calculationController.GetComponent<CalculateProperty>();

        judgeContainFunc = calculationController.GetComponent<CalculateBoxContainBox>();
        calculateBiggerFunc = calculationController.GetComponent<CalculateBiggerBoxCollider>();

        controller = transform.parent.GetComponent<ObjController>();
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.parent.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BubbleCollider")
        {

            //当たった時に、大きさを比較
            if (calculateBiggerFunc.JudgeWhichBoxIsBigger(this.gameObject, other.gameObject))
            {
                //泡に破裂命令
                other.transform.parent.GetComponent<BubbleSetController>().DestroyBubbleSet();
            }
            else
            {
                canBeContained = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BubbleCollider")
        {
            if (judgeContainFunc.JudgeIsBoxBContainBoxA(this.gameObject, other.gameObject))
            {
                if (canBeContained)
                {
                    SetBoxAndBubbleVelocity(calculationProperty.UpForce, this.gameObject, other.gameObject);
                    canBeContained = false;
                }
            }
            else
            {
                //一定時間後に、泡を破滅させる
            }
        }
    }

    private void SetBoxAndBubbleVelocity(float _upForce, GameObject _enemy, GameObject _boxCollider)
    {
        if (_boxCollider == null)
            return;

        //FIXME:ずっと探すではなく、一回だけにする
        if (_boxCollider.transform.parent.Find("Bubble"))
        {
            Vector3 upForce = Vector3.up * _upForce;
            GameObject bubble = _boxCollider.transform.parent.Find("Bubble").gameObject;
            bubble.GetComponent<BubbleController>().SetRigidbodyVelocityOnce(upForce);
        }
        controller.SetCenterPos(_boxCollider.transform.parent.Find("Bubble"));
    }
}
