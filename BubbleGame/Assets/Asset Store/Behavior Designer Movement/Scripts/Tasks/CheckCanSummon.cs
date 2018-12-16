using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class CheckCanSummon : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}