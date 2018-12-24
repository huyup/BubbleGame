using UnityEngine;
using UnityEngine.AI;
namespace BehaviorDesigner.Runtime.Tasks
{
    public class CheckCanStopBehavior : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (!GetComponent<NavMeshAgent>().enabled)
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