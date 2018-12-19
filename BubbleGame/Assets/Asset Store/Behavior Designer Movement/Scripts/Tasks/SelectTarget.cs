using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class SelectTarget : Conditional
    {
        public SharedGameObject target;
        public SharedGameObject returnTarget;

        public override TaskStatus OnUpdate()
        {
            returnTarget.Value = target.Value;
            if (returnTarget != null)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}