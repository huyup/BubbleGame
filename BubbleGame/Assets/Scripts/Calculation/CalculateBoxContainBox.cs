using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// XXX:計算が間違う場合がある
/// </summary>
public class CalculateBoxContainBox : MonoBehaviour
{
    /// <summary>
    /// 物体bの中に物体aがあるかどうかをチェック
    /// </summary>
    public bool JudgeIsBoxBContainBoxA(GameObject _boxA, GameObject _boxB)
    {
        //FIXME:ここのreturn文がおかしい
        if (_boxA == null || _boxB == null)
            return false;

        bool isInsideInLength = false;
        bool isInsideInHeight = false;
        bool isInsideInDepth = false;

        isInsideInLength = JudgeIsInsideInLength(_boxA.GetComponent<CalculateBoxColliderVertices>().LengthVertices,
    _boxB.GetComponent<CalculateBoxColliderVertices>().LengthVertices);

        isInsideInHeight = JudgeIsInsideInHeight(_boxA.GetComponent<CalculateBoxColliderVertices>().HeightVertices,
    _boxB.GetComponent<CalculateBoxColliderVertices>().HeightVertices);

        isInsideInDepth = JudgeIsInsideInDepth(_boxA.GetComponent<CalculateBoxColliderVertices>().DepthVertices,
    _boxB.GetComponent<CalculateBoxColliderVertices>().DepthVertices);

        if (isInsideInLength && isInsideInHeight && isInsideInDepth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// [0]はmin,[1]はmax
    /// </summary>
    private bool JudgeIsInsideInLength(Vector3[] _boxLengthVertices, Vector3[] _bubbleLengthVertices)
    {
        bool isVertex1Inside = false;
        bool isVertex2Inside = false;

        if (_boxLengthVertices[0].x >= _bubbleLengthVertices[0].x &&
            _boxLengthVertices[0].x <= _bubbleLengthVertices[1].x)
        {
            isVertex1Inside = true;
        }

        if (_boxLengthVertices[1].x >= _bubbleLengthVertices[0].x &&
            _boxLengthVertices[1].x <= _bubbleLengthVertices[1].x)
        {
            isVertex2Inside = true;
        }

        if (isVertex1Inside && isVertex2Inside)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool JudgeIsInsideInHeight(Vector3[] _boxHeightVertices, Vector3[] _bubbleHeightVertices)
    {
        bool isVertex1Inside = false;
        bool isVertex2Inside = false;


        if (_boxHeightVertices[0].y >= _bubbleHeightVertices[0].y &&
            _boxHeightVertices[0].y <= _bubbleHeightVertices[1].y)
        {
            isVertex1Inside = true;
        }

        if (_boxHeightVertices[1].y >= _bubbleHeightVertices[0].y &&
            _boxHeightVertices[1].y <= _bubbleHeightVertices[1].y)
        {
            isVertex2Inside = true;
        }

        if (isVertex1Inside && isVertex2Inside)
        {

            return true;
        }
        else
            return false;

    }

    private bool JudgeIsInsideInDepth(Vector3[] _boxDepthVertices, Vector3[] _bubbleDepthVertices)
    {
        bool isVertex1Inside = false;
        bool isVertex2Inside = false;

        if (_boxDepthVertices[0].z >= _bubbleDepthVertices[0].z &&
            _boxDepthVertices[0].z <= _bubbleDepthVertices[1].z)
        {
            isVertex1Inside = true;
        }

        if (_boxDepthVertices[1].z >= _bubbleDepthVertices[0].z &&
            _boxDepthVertices[1].z <= _bubbleDepthVertices[1].z)
        {
            isVertex2Inside = true;
        }

        if (isVertex1Inside && isVertex2Inside)
        {
            return true;
        }
        else
            return false;

    }
}
