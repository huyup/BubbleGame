using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjJudgeCollision : MonoBehaviour
{
    private CalculationRef calculationController;

    ObjFloatByContain floatByContain;

    private ObjController controller;

    private bool canBeContained = false;

    // Use this for initialization
    void Start()
    {
        controller = transform.parent.GetComponent<ObjController>();
        calculationController = GameObject.Find("CalculationController").GetComponent<CalculationRef>();

        floatByContain = transform.parent.GetComponent<ObjFloatByContain>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BubbleCollider")
        {
            //当たった時に、大きさを比較
            if (calculationController.GetBiggerFunction().JudgeWhichBoxIsBigger(this.gameObject, other.gameObject))
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
            if (calculationController.GetContainFunction().JudgeIsBoxBContainBoxA(this.gameObject, other.gameObject))
            {
                if (canBeContained)
                {
                    SetBoxAndBubbleFloat(this.gameObject, other.gameObject);
                    canBeContained = false;
                }
            }
            else
            {
                //一定時間後に、泡を破滅させる
            }
        }
    }

    private void SetBoxAndBubbleFloat(GameObject _enemy, GameObject _boxCollider)
    {
        if (_boxCollider == null)
            return;
        if (controller.ObjState == ObjState.OnGround)
            floatByContain.FloatByContain(_boxCollider.transform.parent.Find("Bubble"));
    }
}
