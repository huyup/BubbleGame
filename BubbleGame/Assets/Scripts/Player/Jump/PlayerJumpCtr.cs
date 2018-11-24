using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCtr : PlayerController
{
    [SerializeField]
    protected GroundDetector GroundDetector;
    #region ジャンプ用メソッド

    public override void OnInitialize()
    {
        int groundLayer = (1 << 9) | (1 << 10);
        GroundDetector = GetComponent<GroundDetector>();
        GroundDetector.Initialize(0.25f, 2.0f, 0.01f, 0.05f, groundLayer);
    }

    public override void OnUpdate()
    {
        ChangeGravity();
        GroundDetector.UpdateDetection();
    }

    public void Jump()
    {
        if (!GroundDetector.IsHit)
            return;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 velocity = transform.up * Status.JumpPower * Time.fixedDeltaTime * 60;
        GetComponent<Rigidbody>().velocity = velocity;

    }

    private void ChangeGravity()
    {
        ///FIXME:設定に重力を変えられないか？
        //重力を増やせる
        GetComponent<Rigidbody>().velocity += Physics.gravity * Status.FactorToCalGravity * Time.fixedDeltaTime * 60;
    }

    #endregion
}
