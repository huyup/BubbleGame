using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using DG.Tweening;

public class MovingNeedleTrap : TrapBase
{
    [SerializeField]
    private Transform moveTransform;
    
    [SerializeField]
    private float duration;
    
    [SerializeField]
    private Vector3 targetLocalPosition;

    public override void OnStart()
    {
        //moveTransform.DOLocalMove(targetLocalPosition, duration)
        //    .SetEase(Ease.InOutCubic)
        //    .SetLoops(-1, LoopType.Yoyo);
    }
}
