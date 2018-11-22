﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJudgeCollision : MonoBehaviour
{
    GameObject calculationController;
    CalculationProperty calculationProperty;

    JudgeBoxBContainBoxA judgeContainFunc;
    JudgeBiggerBoxCollider judgeBiggerFunc;

    EnemyController controller;
    EnemyMove enemyMove;

    /// <summary>
    /// FIXME:ここをenumにできないか
    /// </summary>
    bool isEnemyBiggerThanBubble = false;
    bool isBubbleContainEnemy = false;

    bool canAddUpForce = false;

    GameObject bubble;
    // Use this for initialization
    void Start()
    {
        enemyMove = transform.parent.GetComponent<EnemyMove>();

        calculationController = GameObject.Find("CalculationController");

        calculationProperty = calculationController.GetComponent<CalculationProperty>();

        judgeContainFunc = calculationController.GetComponent<JudgeBoxBContainBoxA>();
        judgeBiggerFunc = calculationController.GetComponent<JudgeBiggerBoxCollider>();

        controller = transform.parent.GetComponent<EnemyController>();
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
            if (judgeBiggerFunc.JudegeWhichBoxIsBigger(this.gameObject, other.gameObject))
            {
                //泡に破裂命令
                other.transform.parent.GetComponent<BubbleSetController>().DestoryNow();
                enemyMove.ChangeSpeed();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BubbleCollider")
        {
            isBubbleContainEnemy = judgeContainFunc.JudgeIsBoxBContainBoxA(this.gameObject, other.gameObject);
            if (isBubbleContainEnemy)
            {
                canAddUpForce = true;
                other.transform.parent.GetComponent<BubbleSetController>().SaveInsideObj(transform.parent.gameObject);
                bubble = other.gameObject;
            }
            else
            {
                //一定時間後に、泡を破滅させる
            }
        }
    }
    private void Update()
    {
        if(canAddUpForce)
        {
            SetBoxAndBubbleVelocity(calculationProperty.UpForce, this.gameObject, bubble);
        }
    }
    /// <summary>
    /// FIXME:Controllerに置けないか？可変性がない
    /// </summary>
    /// <param name="_upForce"></param>
    /// <param name="_enemy"></param>
    /// <param name="_boxColider"></param>
    private void SetBoxAndBubbleVelocity(float _upForce, GameObject _enemy, GameObject _boxColider)
    {
        if (_boxColider == null)
            return;

        //FIXME:ずっと探すではなく、一回だけにする
        if (_boxColider.transform.parent.Find("Bubble"))
        {
            Vector3 upForce = Vector3.up * _upForce;
            GameObject bubble = _boxColider.transform.parent.Find("Bubble").gameObject;
            bubble.GetComponent<BubbleController>().SetRigibodyVelocityOnce(upForce);
        }

        //controller.SetRigibodyVelocityOnce(upForce);
        controller.MoveToCenterPos(_boxColider.transform.parent.Find("Bubble"));
    }
}
