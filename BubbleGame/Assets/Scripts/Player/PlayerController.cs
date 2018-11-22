﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerController : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// アタッチするコンポーネント
    /// </summary>
    Rigidbody rb;
    Animator animator;
    PlayerStatus status;
    GroundDetector groundDetector;
    /// <summary>
    /// 移動 
    /// </summary>
    float moveSpeed;

    //入力前の位置
    Vector3 prevPlayerPos;

    //無敵かどうか
    bool isVincible = false;

    #endregion

    #region 初期化
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();

        moveSpeed = status.RunSpeed;

        prevPlayerPos = transform.position;

        int groundLayer = (1 << 9) | (1 << 10);

        groundDetector = GetComponent<GroundDetector>();
        groundDetector.Initialize(0.25f, 2.0f, 0.01f, 0.05f, groundLayer);
    }


    #endregion

    #region Updateで状態チェック
    private void Update()
    {
        groundDetector.UpdateDetection();
        CheckDied();
    }
    private void FixedUpdate()
    {
        //重力を増やせる
        rb.velocity += Physics.gravity * status.FactorToCalGravity;
    }
    #endregion

    #region アニメーション用メソッド
    public void SetMoveAnimation()
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - prevPlayerPos;

        if (direction.magnitude > 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    #endregion

    #region 回転用メソッド
    public void RotatePlayer(float _inputH2, float _inputV2, float _maxControllerLerance)
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - prevPlayerPos;
        //最新の位置-入力前の位置=方向
        Vector3 directionXZ = direction - new Vector3(0, direction.y, 0);

        if (directionXZ.magnitude > 0)
        {
            //RotateCharaterByOneAxis 左スティックだけ採用するときはこちらを使う
            RotateCharaterByTwoAxis(directionXZ, _inputH2, _inputV2, _maxControllerLerance);
        }
        else
        {
            //動かないときでも方向転換できるように
            RotateCharacterByAxis2(_inputH2, _inputV2);
        }

        //計算後、入力前の位置を更新する
        prevPlayerPos = transform.position;
    }


    private void RotateCharaterByOneAxis(Vector3 _direction, float _maxControllerLerance)
    {
        if ((_direction.magnitude > _maxControllerLerance))
            RotateCharacterByAxis1(_direction);
    }

    private void RotateCharaterByTwoAxis(Vector3 _direction, float _inputH2, float _inputV2, float _maxControllerLerance)
    {
        if ((_direction.magnitude > _maxControllerLerance) &&
    (_inputH2 == 0 && _inputV2 == 0))
            RotateCharacterByAxis1(_direction);
        else
            RotateCharacterByAxis2(_inputH2, _inputV2);
    }

    private void RotateCharacterByAxis1(Vector3 _direction)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3
(_direction.x, 0, _direction.z));
    }

    private void RotateCharacterByAxis2(float _h, float _v)
    {
        //なぜか右のスティックが上下反転しているため、
        //vの値をマイナスにした
        Vector3 velocity = new Vector3(_h, 0, -_v) * Time.deltaTime;

        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3
    (velocity.x, 0, velocity.z));
    }
    #endregion

    #region 移動用メソッド
    public void MoveByRigidBody(float _h, float _v, float _maxControllerLerance)
    {
        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

        if (velocity.magnitude > _maxControllerLerance)
            //現在の位置＋入力した数値の場所に移動する
            rb.MovePosition(transform.position + velocity);
    }
    #endregion

    #region ジャンプ用メソッド
    public void Jump()
    {
        if (!groundDetector.IsHit)
            return;
        rb.velocity = Vector3.zero;
        Vector3 velocity = transform.up * status.JumpPower * Time.fixedDeltaTime * 60;
        rb.velocity = velocity;
    }
    #endregion

    #region ダメージ用
    public void Damgae()
    {
        if (isVincible || status.nowHp <= 0)
            return;
        if (status.nowHp > 0)
            status.nowHp--;

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Renderer renderer;
        renderer = transform.Find("PlayerMesh").GetComponent<Renderer>();
        for (int i = 0; i < status.InvincibleTotalTime; i++)
        {
            renderer.enabled = !renderer.enabled;
            isVincible = true;
            yield return new WaitForSeconds(status.InvincibleInterval);
        }
        isVincible = false;
    }

    public void CheckDied()
    {
        if (status.nowHp <= 0)
            this.gameObject.SetActive(false);
    }
    #endregion


}
