using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXME:体積で大きさを比較に変更
/// </summary>
public class CalculateBiggerBoxCollider : MonoBehaviour
{
    enum BiggerObject
    {
        Bubble,
        Obj,
        None,
    }


    private BiggerObject biggerObject;

    /// <summary>
    /// 物体が泡より大きい場合、Trueを返す、
    /// _boxAが_boxBより小さい場合、Falseを返す、
    /// </summary>
    public bool JudgeWhichBoxIsBigger(GameObject _Obj, GameObject _bubble)
    {
        //FIXME:ここのreturn文がおかしい
        if (_Obj == null || _bubble == null)
            return false;

        bool isObjBiggerThanBubble = false;

        bool isObjLongerThanBubble = false;
        bool isObjHigherThanBubble = false;
        bool isObjDeeperThanBubble = false;

        isObjLongerThanBubble = JudgeTwoBoxColliderInLength(_Obj.GetComponent<CalculateBoxColliderVertices>().LengthVertices,
            _bubble.GetComponent<CalculateBoxColliderVertices>().LengthVertices);

        isObjHigherThanBubble = JudgeTwoBoxColliderInHeight(_Obj.GetComponent<CalculateBoxColliderVertices>().HeightVertices,
    _bubble.GetComponent<CalculateBoxColliderVertices>().HeightVertices);

        isObjDeeperThanBubble = JudgeTwoBoxColliderInDepth(_Obj.GetComponent<CalculateBoxColliderVertices>().DepthVertices,
    _bubble.GetComponent<CalculateBoxColliderVertices>().DepthVertices);

        if (isObjDeeperThanBubble && isObjHigherThanBubble && isObjLongerThanBubble)
        {
            isObjBiggerThanBubble = true;
        }
        else if (!isObjDeeperThanBubble && !isObjHigherThanBubble && !isObjLongerThanBubble)
        {

            isObjBiggerThanBubble = false;
        }

        Debug.Log("Return"+ isObjBiggerThanBubble);
        return isObjBiggerThanBubble;
    }

    private bool JudgeTwoBoxColliderInLength(Vector3[] _boxLengthVertices, Vector3[] _bubbleLengthVertices)
    {
        bool isBoxLoggerThanBubble = false;

        float boxLength = (_boxLengthVertices[0] - _boxLengthVertices[1]).magnitude ;
        float bubbleLength = (_bubbleLengthVertices[0] - _bubbleLengthVertices[1]).magnitude;
        Debug.Log("boxLength"+ boxLength);
        Debug.Log("bubbleLength" + bubbleLength);
        if (boxLength > bubbleLength)
        {
            isBoxLoggerThanBubble = true;
        }
        else
        {
            isBoxLoggerThanBubble = false;
        }

        return isBoxLoggerThanBubble;
    }

    private bool JudgeTwoBoxColliderInHeight(Vector3[] _boxHeightVertices, Vector3[] _bubbleHeightVertices)
    {
        bool isBoxHigherThanBubble = false;

        float boxHeight = (_boxHeightVertices[0] - _boxHeightVertices[1]).magnitude;
        float bubbleHeight = (_bubbleHeightVertices[0] - _bubbleHeightVertices[1]).magnitude;
        Debug.Log("boxHeight" + boxHeight);
        Debug.Log("bubbleHeight" + bubbleHeight);
        if (boxHeight > bubbleHeight)
        {
            isBoxHigherThanBubble = true;
        }
        else
        {
            isBoxHigherThanBubble = false;
        }

        return isBoxHigherThanBubble;
    }

    private bool JudgeTwoBoxColliderInDepth(Vector3[] _boxDepthVertices, Vector3[] _bubbleDepthVertices)
    {
        bool isBoxDeeperThanBubble = false;

        float boxDepth = (_boxDepthVertices[0] - _boxDepthVertices[1]).magnitude ;
        float bubbleDepth = (_bubbleDepthVertices[0] - _bubbleDepthVertices[1]).magnitude;
        Debug.Log("boxDepth" + boxDepth);
        Debug.Log("bubbleDepth" + bubbleDepth);
        if (boxDepth > bubbleDepth)
        {
            isBoxDeeperThanBubble = true;
        }
        else
        {
            isBoxDeeperThanBubble = false;
        }

        return isBoxDeeperThanBubble;
    }
}
