using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class CheckCanSummon : Conditional
    {
        public SharedInt numToSummon = 2;

        public override TaskStatus OnUpdate()
        {
            if (!StageManager.HasInstance)
                return TaskStatus.Running;

            if (StageManager.Instance.GetAllEnemyCount() <= numToSummon.Value)
                return TaskStatus.Success;
            else
                return TaskStatus.Failure;
        }
    }
}