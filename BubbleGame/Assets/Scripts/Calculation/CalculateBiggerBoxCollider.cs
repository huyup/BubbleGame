using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BiggerObject
{
    None,
    Bubble,
    Obj,
}
/// <summary>
/// 体積計算はゲーム体験とは違うため、辺長で計算する（深度抜き）
/// </summary>
public class CalculateBiggerBoxCollider : MonoBehaviour
{
    private BiggerObject biggerObject;
    
    public BiggerObject JudgeWhichBoxIsBigger(GameObject _Obj, GameObject _bubble)
    {
        CalculateBoxColliderVertices objVertices = _Obj.GetComponent<CalculateBoxColliderVertices>();
        CalculateBoxColliderVertices bubbleVertices = _bubble.GetComponent<CalculateBoxColliderVertices>();

        float objLength = (objVertices.LengthVertices[0] - objVertices.LengthVertices[1]).magnitude;
        float objHeight = (objVertices.HeightVertices[0] - objVertices.HeightVertices[1]).magnitude;

        float bubbleLength = (bubbleVertices.LengthVertices[0] - bubbleVertices.LengthVertices[1]).magnitude;
        float bubbleHeight = (bubbleVertices.HeightVertices[0] - bubbleVertices.HeightVertices[1]).magnitude;
        
        if (bubbleLength >= objLength && bubbleHeight >= objHeight)
        {
            biggerObject = BiggerObject.Bubble;
        }
        else
            biggerObject = BiggerObject.Obj;

        return biggerObject;
    }
    
}
