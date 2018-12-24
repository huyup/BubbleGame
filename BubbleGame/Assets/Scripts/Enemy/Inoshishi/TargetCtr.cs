using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
public class TargetCtr : MonoBehaviour
{
    [SerializeField]
    private List<BehaviorTree> behaviorsToSetTarget = new List<BehaviorTree>();

    [SerializeField]
    private BehaviorTree behaviorToSetStartPos;

    private GameObject nowTarget;

    private GameObject startTarget;

    private bool canSetNowTarget;
    public void TryToSetTarget(GameObject _target)
    {
        nowTarget = _target;
        canSetNowTarget = true;
    }

    public void SetStartPos(GameObject _startPos)
    {
        startTarget = _startPos;

        behaviorToSetStartPos.SetVariableValue("StartTarget", startTarget);
    }
    private void Update()
    {
        if (canSetNowTarget)
        {
            foreach (var behaviour in behaviorsToSetTarget)
            {
                behaviour.SetVariableValue("Target", nowTarget);
            }
            canSetNowTarget = false;
        }
    }
}
