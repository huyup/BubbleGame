using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class CheckCanStartWander : Conditional
    {
        public SharedGameObject Target;

        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(Target.Value.transform.position, transform.position) < 3f)
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