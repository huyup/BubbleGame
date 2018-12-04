using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJudgeCollision : MonoBehaviour
{
    private CalculationRef calculationController;

    private EnemyController controller;

    [SerializeField]
    private bool canBeContained = false;

    // Use this for initialization
    void Start()
    {
        calculationController = GameObject.Find("CalculationController").GetComponent<CalculationRef>();

        controller = transform.parent.GetComponent<EnemyController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.layer == 13/*JudgeBox*/ )
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
        if (other.gameObject.layer == 13/*JudgeBox*/ )
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
                //TODO:ここに含まれていない状態の処理を入れる
            }
        }
    }

    private void SetBoxAndBubbleFloat(GameObject _enemy, GameObject _boxCollider)
    {
        if (_boxCollider == null)
            return;

        //FIXME:ずっと探すではなく、一回だけにする
        if (_boxCollider.transform.parent.Find("Bubble"))
        {
            GameObject bubble = _boxCollider.transform.parent.Find("Bubble").gameObject;
            bubble.GetComponent<BubbleController>().SetFloatVelocityToBubble();
        }

        controller.FloatByContain(_boxCollider.transform.parent.Find("Bubble"));
    }
}
