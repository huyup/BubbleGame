using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class CheckCanSummon : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (!StageManager.HasInstance)
                return TaskStatus.Running;

            if (StageManager.Instance.GetAllEnemyCount() < 2)
                return TaskStatus.Success;
            else
                return TaskStatus.Failure;
        }
    }
}