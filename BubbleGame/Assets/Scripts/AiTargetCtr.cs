using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
public class AiTargetCtr : MonoBehaviour
{
    [SerializeField]
    private List<BehaviorTree> behaviors = new List<BehaviorTree>();


    [SerializeField]
    private GameObject nowTarget;

    private bool canSetTarget;

    public void SetTarget(GameObject _target)
    {
        Debug.Log("Set");
        nowTarget = _target;
        canSetTarget = true;
    }
    private void Update()
    {
        if (canSetTarget)
        {
            Debug.Log("behaviourSet");
            foreach (var behaviour in behaviors)
            {
                behaviour.SetVariableValue("Target", nowTarget);
            }
            canSetTarget = false;
        }
    }
}
