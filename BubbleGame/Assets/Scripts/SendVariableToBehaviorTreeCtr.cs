using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
public class SendVariableToBehaviorTreeCtr : MonoBehaviour
{
    [SerializeField]
    private List<BehaviorTree> behaviorsToSetTarget = new List<BehaviorTree>();
    
    [SerializeField]
    private GameObject nowTarget;

    private GameObject startTarget;

    private bool canSetTarget;

    [SerializeField]
    private BehaviorTree behaviorToSetStartPos;

    public void SetTarget(GameObject _target)
    {
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
