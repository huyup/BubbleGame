using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateCtr : PlayerController
{
    [HideInInspector]
    PlayerController ctr;
    public void Rotate(float _inputH2, float _inputV2, float _maxControllerLerance, Vector3 _prevInputPlayerPos)
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - _prevInputPlayerPos;
        //最新の位置-入力前の位置=方向
        Vector3 directionXZ = direction - new Vector3(0, direction.y, 0);

        if (directionXZ.magnitude > 0)
        {
            RotateCharacterByTwoAxis(directionXZ, _inputH2, _inputV2, _maxControllerLerance);
        }
        else
        {
            //動かないときでも方向転換できるように
            RotateCharacterByAxis2(_inputH2, _inputV2);
        }
    }

    private void RotateCharacterByTwoAxis(Vector3 _direction, float _inputH2, float _inputV2, float _maxControllerLerance)
    {
        if ((_direction.magnitude > _maxControllerLerance) &&
            (Mathf.Abs(_inputH2) <= 0.001f && Mathf.Abs(_inputV2) <= 0.001f))
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
        Vector3 velocity = new Vector3(_h, 0, -_v) * Time.fixedDeltaTime;

        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3
                (velocity.x, 0, velocity.z));
    }
}
