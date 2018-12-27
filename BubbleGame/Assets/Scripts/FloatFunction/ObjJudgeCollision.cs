using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjJudgeCollision : MonoBehaviour
{
    private CalculationRef calculationController;

    ObjFloatByContain floatByContain;

    private ObjController controller;

    private bool canBeContained = false;

    private BiggerObject biggerObject;
    // Use this for initialization
    void Start()
    {
        controller = transform.parent.GetComponent<ObjController>();
        calculationController = GameObject.Find("CalculationController").GetComponent<CalculationRef>();

        floatByContain = transform.parent.GetComponent<ObjFloatByContain>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BubbleCollider"))
        {
            //何も入っていない状態だけ、処理する
            if (other.GetComponent<BubbleJudgeCollider>().CanAddObjInside)
            {
                biggerObject = calculationController.GetBiggerFunction()
                    .JudgeWhichBoxIsBigger(this.gameObject, other.gameObject);
                //当たった時に、大きさを比較
                if (biggerObject == BiggerObject.Obj)
                {
                    if (other.transform.parent.Find("Bubble").GetComponent<BubbleController>().GetBubbleState() !=
                        BubbleState.Creating)
                    {
                        //泡に破裂命令
                        other.transform.root.GetComponent<BubbleSetController>().DestroyBubbleSet();
                    }
                }
                else if (biggerObject == BiggerObject.Bubble)
                {
                    other.GetComponent<BubbleJudgeCollider>().AddObjInside();
                    canBeContained = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BubbleCollider"))
        {
            //何も入っていない状態だけ、処理する
            if (other.GetComponent<BubbleJudgeCollider>().CanAddObjInside)
            {
                biggerObject = calculationController.GetBiggerFunction()
                    .JudgeWhichBoxIsBigger(this.gameObject, other.gameObject);
                //当たった時に、大きさを比較
                if (biggerObject == BiggerObject.Obj)
                {
                    if (other.transform.parent.Find("Bubble").GetComponent<BubbleController>().GetBubbleState() !=
                        BubbleState.Creating)
                    {
                        //泡に破裂命令
                        other.transform.root.GetComponent<BubbleSetController>().DestroyBubbleSet();
                    }
                }
                else if (biggerObject == BiggerObject.Bubble)
                {
                    other.GetComponent<BubbleJudgeCollider>().AddObjInside();
                    canBeContained = true;
                }
            }
        }
        if (other.gameObject.CompareTag("BubbleCollider"))
        {
            if (calculationController.GetContainFunction().JudgeIsBoxBContainBoxA(this.gameObject, other.gameObject))
            {
                if (canBeContained)
                {
                    SetBoxAndBubbleFloat(this.gameObject, other.gameObject);
                    canBeContained = false;
                }
            }
        }
    }
    private void SetBoxAndBubbleFloat(GameObject _enemy, GameObject _boxCollider)
    {
        if (_boxCollider == null)
            return;

        Transform bubble = _boxCollider.transform.parent.Find("Bubble");

        if (controller.ObjState == ObjState.OnGround)
            floatByContain.FloatByContain(bubble);

        bubble.GetComponent<BubbleController>().StopBubble();

    }
}
