﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargSearch : EnemySearch
{
    private WargsStatus status;

    private WargController wargController;

    private class Targets
    {
        public GameObject Target;
        public float Distance;
        public bool IsVisible;
    }

    private Targets[] targets = new Targets[GameSetting.MaxPlayerNum];

    private bool isSetFinished = false;

    // Use this for initialization
    void Start()
    {
        wargController = transform.parent.GetComponent<WargController>();
        status = transform.root.GetComponent<WargsStatus>();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = new Targets();
            targets[i].Target = GameObject.Find("Player" + (i + 1).ToString());
            targets[i].Distance = 0;
            targets[i].IsVisible = false;
        }
    }

    /// <summary>
    /// 視野を模倣する索敵メソッド
    /// FIXME:簡潔化すべき
    /// </summary>
    public void SearchByEye()
    {
        if (targets.Length == 0)
        {
            Debug.Log("プレイヤー名を設定してください");
            return;
        }

        if (!isSetFinished)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                float angle = 0;
                angle = CalculateAngleFromEyeToTarget(targets[i].Target.transform);
                targets[i].Distance = Vector3.Distance(
                    transform.parent.position - new Vector3(0, transform.parent.position.y),
                    targets[i].Target.transform.position - new Vector3(0, targets[i].Target.transform.position.y));
                if (angle <= status.MaxViewAngle && targets[i].Distance <= status.EyeDistance)
                {
                    targets[i].IsVisible = true;
                }
            }

            float nearDistance = 999;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].IsVisible)
                {
                    if (targets[i].Distance < nearDistance)
                        nearDistance = targets[i].Distance;
                }
            }

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].IsVisible && targets[i].Distance - nearDistance == 0)
                {
                    wargController.SetAttackTarget(targets[i].Target.transform);
                    SetFinished();
                    break;
                }
            }
        }
    }

    private float CalculateAngleFromEyeToTarget(Transform _targetTransform)
    {
        if (_targetTransform == null)
            return 0;
        //前に向く視線ベクトル
        Vector3 eyeDirection = transform.forward;

        //ターゲットを指すベクトル
        Vector3 targetDirection = _targetTransform.position - transform.position;

        float angleFromEyeToTarget = Vector3.Angle(targetDirection, eyeDirection);

        return angleFromEyeToTarget;
    }

    void OnTriggerStay(Collider other)
    {
        // Playerタグをターゲットにする
        if (other.tag == "Player" && !isSetFinished)
        {
            wargController.SetAttackTarget(other.transform);
            isSetFinished = true;
        }
    }

    void SetFinished()
    {
        isSetFinished = true;
    }

    public void Research()
    {
        wargController.SetAttackTarget(null);
        isSetFinished = false;
    }

    #region Debug用

    /// <summary>
    /// TODO:ここにデバッグ用のrayを表示する
    /// </summary>
    private void DrawRayAsEyeArea()
    {
    }

    #endregion
}