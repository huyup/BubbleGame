﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJudgeCollision : MonoBehaviour
{
    private CalculationRef calculationController;

    [SerializeField]
    private EnemyFunctionRef enemyFunctionRef;

    [SerializeField]
    private bool canBeContained = false;

    // Use this for initialization
    void Start()
    {
        calculationController = GameObject.Find("CalculationController").GetComponent<CalculationRef>();

    }
    private void OnTriggerEnter(Collider _other)
    {

        if (_other.gameObject.layer == 13/*JudgeBox*/ )
        {
            //当たった時に、大きさを比較
            if (calculationController.GetBiggerFunction().JudgeWhichBoxIsBigger(this.gameObject, _other.gameObject))
            {
                //泡に破裂命令
                _other.transform.parent.GetComponent<BubbleSetController>().DestroyBubbleSet();
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

        enemyFunctionRef.GetEnemyController().FloatByContain(_boxCollider.transform.parent.Find("Bubble"));
    }
}
