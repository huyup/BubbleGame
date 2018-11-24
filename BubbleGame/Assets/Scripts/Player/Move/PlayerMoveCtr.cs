using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtr : PlayerController {

    #region 移動用メソッド
    public void MoveByRigidBody(float _h, float _v, float _maxControllerTolerance, Vector3 _prevPosition)
    {
        Vector3 velocity = new Vector3(_h * Status.RunSpeed, 0, _v * Status.RunSpeed) * Time.fixedDeltaTime;

        if (velocity.magnitude > _maxControllerTolerance)
            //現在の位置＋入力した数値の場所に移動する
            Rb.MovePosition(transform.position + velocity);

        base.GetAnimatorCtr().SetMoveAnimation(_prevPosition);
    }
    #endregion
}
