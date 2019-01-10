using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjJudgeCollision : MonoBehaviour
{
    private CalculationRef calculationController;

    ObjFloatByContain floatByContain;

    private ObjController controller;

    private bool canSetFloat = false;

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
            canSetFloat = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BubbleCollider"))
        {
            var bubbleCtr = other.transform.parent.Find("Bubble").GetComponent<BubbleController>();
            if (bubbleCtr.GetBubbleState() != BubbleState.Creating)
            {
                biggerObject = calculationController.GetBiggerFunction()
                    .JudgeWhichBoxIsBigger(this.gameObject, other.gameObject);
                if (biggerObject == BiggerObject.Obj)
                {
                    if (other.transform.root.Find("Bubble"))
                    {
                        //泡に破裂命令
                        other.transform.root.GetComponent<BubbleSetController>().DestroyBubbleSet();
                    }
                }
            }
            //何も入っていない状態だけ、処理する
            if (!other.GetComponent<BubbleJudgeCollider>().HadObjInside &&
                bubbleCtr.GetBubbleState() != BubbleState.BePressed &&
                bubbleCtr.GetBubbleState() != BubbleState.BeTakeIn)
            {
                if (calculationController.GetContainFunction()
                    .JudgeIsBoxBContainBoxA(this.gameObject, other.gameObject))
                {
                    if (canSetFloat)
                    {
                        SetBoxAndBubbleFloat(this.gameObject, other.gameObject);
                        other.GetComponent<BubbleJudgeCollider>().AddObjInside();
                        canSetFloat = false;
                    }
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
    }
}
