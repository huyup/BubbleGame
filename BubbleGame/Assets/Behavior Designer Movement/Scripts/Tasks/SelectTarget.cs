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
            if (returnTarget.Value.GetComponent<PlayerStatus>().nowHp > 0)
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