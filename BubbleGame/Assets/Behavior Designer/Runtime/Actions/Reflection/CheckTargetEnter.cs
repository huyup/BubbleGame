using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class CheckTargetEnter : Conditional
    {
        public SharedGameObject Target;

        private bool hasGetTarget = false;
        public override TaskStatus OnUpdate()
        {
            if (hasGetTarget)
                return TaskStatus.Success;
            else
            {
                return TaskStatus.Failure;
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            hasGetTarget = true;
        }
        
    }
}