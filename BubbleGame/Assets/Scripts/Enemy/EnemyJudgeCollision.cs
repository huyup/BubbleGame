using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJudgeCollision : MonoBehaviour
{
    GameObject calculationController;
    CalculateProperty calculationProperty;

    CalculateBoxContainBox judgeContainFunc;
    CalculateBiggerBoxCollider calculateBiggerFunc;

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

        calculationProperty = calculationController.GetComponent<CalculateProperty>();

        judgeContainFunc = calculationController.GetComponent<CalculateBoxContainBox>();
        calculateBiggerFunc = calculationController.GetComponent<CalculateBiggerBoxCollider>();

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
            if (calculateBiggerFunc.JudgeWhichBoxIsBigger(this.gameObject, other.gameObject))
            {
                //泡に破裂命令
                other.transform.parent.GetComponent<BubbleSetController>().DestroyBubbleSet();
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
                bubble = other.gameObject;
            }
            else
            {
                //TODO:ここに含まれていない状態の処理を入れる
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
            bubble.GetComponent<BubbleController>().SetRigidbodyVelocityOnce(upForce);
        }

        controller.InitFloatFunction(_boxColider.transform.parent.Find("Bubble"));
        canAddUpForce = false;
    }
}
