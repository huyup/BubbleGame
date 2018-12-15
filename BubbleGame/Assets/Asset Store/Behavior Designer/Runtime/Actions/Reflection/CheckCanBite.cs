using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class CheckCanBite : Conditional
    {
        public SharedGameObject Target;
        public SharedFloat AttackRange;
        public override void OnStart()
        {
        }
        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(Target.Value.transform.position, transform.position) < AttackRange.Value)
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