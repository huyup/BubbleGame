using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // 重力値.
    private const float GravityPower = 9.8f;

    //　目的地についたとみなす停止距離.
    private const float StoppingDistance = 0.6f;

    private CharacterController characterController;

    // 目的地.
    public Vector3 Destination;

    // 到着したか（到着した true/到着していない false)
    private bool isArrived = false;

    // 現在の移動速度.
    private Vector3 velocity;

    // 向きを強制的に指示するか.
    private bool forceRotate = false;

    //重力を使うかどうか
    private bool isUseGravity = true;

    // 強制的に向かせたい方向.
    private Vector3 forceRotateDirection;

    private float speed;

    private float initSpeed;

    [SerializeField]
    private EnemyFunctionRef enemyFunctionRef;
    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Destination = transform.position;
        velocity = Vector3.zero;
        speed = enemyFunctionRef.GetEnemyStatus().WalkSpeed;
        initSpeed = enemyFunctionRef.GetEnemyStatus().WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyFunctionRef.GetEnemyStatus().IsFloating && !enemyFunctionRef.GetEnemyStatus().IsDied)
        {
            Move();
        }
    }

    public void BanGravity()
    {
        isUseGravity = false;
    }

    // 目的地を設定する.引数は目的地.
    public void SetDestination(Vector3 _destination)
    {
        isArrived = false;
        Destination = _destination;
    }

    // 指定した向きを向かせる.
    public void SetDirectionXZ(Vector3 _direction)
    {
        forceRotateDirection = _direction;
        forceRotateDirection.y = 0;
        forceRotateDirection.Normalize();
        forceRotate = true;
    }
    public void SetSpeedByHp(int _nowHp, int _maxHp)
    {
        decimal rate = (_nowHp * 100 / _maxHp);

        speed = initSpeed * ((float)rate * 0.01f);
    }
    // 移動をやめる.
    public void StopMove()
    {
        Destination = transform.position; // 現在地点を目的地にしてしまう.
    }

    // 目的地に到着したかを調べる. true　到着した/ false 到着していない.
    public bool CheckIsArrived()
    {
        return isArrived;
    }
    #region function
    private void Move()
    {
        // 移動速度velocityを更新する
        if (characterController.isGrounded)
        {
            CalculateVelocityAndRotation();
        }
        else
        {
            CalculateRotationOnly();
        }

        if (isUseGravity)
        {

            // 接地していたら思いっきり地面に押し付ける.
            // (UnityのCharactorControllerの特性のため）
            Vector3 snapGround = Vector3.zero;
            if (characterController.isGrounded)
                snapGround = Vector3.down;

            // CharacterControllerを使って動かす.
            characterController.Move(velocity * Time.deltaTime + snapGround);
            //重力.
            velocity += Vector3.down * GravityPower * Time.deltaTime;
        }

        if (characterController.velocity.magnitude < 0.1f)
            isArrived = true;

        // 強制的に向きを変えるを解除.
        if (forceRotate && Vector3.Dot(transform.forward, forceRotateDirection) > 0.99f)
            forceRotate = false;
        float distance = Vector3.Distance(transform.position, Destination);

        //　目的地にちかづいたら到着.
        if (distance < StoppingDistance)
            isArrived = true;

    }
    void CalculateVelocityAndRotation()
    {
        //　水平面での移動を考えるのでXZのみ扱う.
        Vector3 destinationXZ = Destination;
        destinationXZ.y = transform.position.y;// 高さを目的地と現在地を同じにしておく.

        float distance = Vector3.Distance(transform.position, destinationXZ);

        //　目的地にちかづいたら到着.
        if (distance < StoppingDistance)
            isArrived = true;

        //********* ここからXZのみで考える. ********
        // 目的地までの距離と方角を求める.
        Vector3 direction = (destinationXZ - transform.position).normalized;

        SetVelocityXZ(direction, speed);
        SetRotation(direction, enemyFunctionRef.GetEnemyStatus().RotateSpeed);
    }

    void CalculateRotationOnly()
    {
        //********* ここからXZのみで考える. ********
        // 目的地までの距離と方角を求める.
        Vector3 direction = (Destination - transform.position).normalized;

        SetRotation(direction, enemyFunctionRef.GetEnemyStatus().RotateSpeed);
    }
    private void SetVelocityXZ(Vector3 _direction, float _walkSpeed)
    {
        // 現在の速度を退避.
        Vector3 currentVelocity = velocity;
        // 移動速度を求める.
        if (isArrived)
            velocity = Vector3.zero;
        else
            velocity = _direction * _walkSpeed;

        // スムーズに補間.
        velocity = Vector3.Lerp(currentVelocity, velocity, Mathf.Min(Time.deltaTime * 5.0f, 1.0f));
        velocity.y = 0;
    }
    private void SetRotation(Vector3 _direction, float _rotateSpeed)
    {
        if (!forceRotate)
        {
            // 向きを行きたい方向に向ける.
            if (velocity.magnitude > 0.1f && !isArrived)
            { // 移動してなかったら向きは更新しない.
                Quaternion characterTargetRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, _rotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            // 強制向き指定.
            Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, _rotateSpeed * Time.deltaTime);
        }
    }
    #endregion

}
