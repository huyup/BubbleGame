using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

using NaughtyAttributes;
public class TargetCtr : MonoBehaviour
{
    [SerializeField]
    private List<BehaviorTree> behaviorsToSetTarget = new List<BehaviorTree>();

    [SerializeField]
    private BehaviorTree behaviorToSetStartPos;

    [SerializeField]
    private int maxTargetNum = 3;

    private GameObject nowTarget;

    private GameObject startTarget;

    private bool canSetTarget;
    
    public void SetTarget(GameObject _target)
    {
        if (canSetTarget)
            return;

        nowTarget = _target;
        canSetTarget = true;
    }

    public void SetStartPos(GameObject _startPos)
    {
        startTarget = _startPos;

        behaviorToSetStartPos.SetVariableValue("StartTarget", startTarget);
    }
    private void Update()
    {
        if (canSetTarget)
        {
            foreach (var behaviour in behaviorsToSetTarget)
            {
                behaviour.SetVariableValue("Target", nowTarget);
            }
            canSetTarget = false;
        }
    }
}
