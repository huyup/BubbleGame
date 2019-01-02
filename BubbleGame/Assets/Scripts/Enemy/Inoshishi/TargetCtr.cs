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
    
    private GameObject startTarget;

    public void TryToSetTarget(GameObject _receiveTarget)
    {
        foreach (var behaviour in behaviorsToSetTarget)
        {

            behaviour.SetVariableValue("Target", _receiveTarget);
        }
    }

    public void SetStartPos(GameObject _startPos)
    {
        startTarget = _startPos;

        behaviorToSetStartPos.SetVariableValue("StartTarget", startTarget);
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    dush.SetVariableValue("Target", player1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    dush.SetVariableValue("Target", player2);
        //}
    }
}
