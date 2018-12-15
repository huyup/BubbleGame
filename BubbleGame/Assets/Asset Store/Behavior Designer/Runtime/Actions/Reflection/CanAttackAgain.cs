using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks
{
    public class CanAttackAgain : Conditional
    {
        public SharedGameObject Target;
        public SharedFloat MaxAttackDistance;

        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(Target.Value.transform.position, transform.position) < MaxAttackDistance.Value)
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