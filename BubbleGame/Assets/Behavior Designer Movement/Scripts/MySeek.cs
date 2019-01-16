using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class MySeek : NavMeshMovement
    {
        [Tooltip("The GameObject that the agent is seeking")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;

        public SharedFloat timeToFinish = 3;

        private float waitDuration;

        private float startTime;

        public override void OnStart()
        {
            base.OnStart();
            startTime = Time.time;

            waitDuration = timeToFinish.Value;
            SetDestination(Target());
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            if (HasArrived() || (startTime + waitDuration < Time.time))
            {
                return TaskStatus.Success;
            }

            SetDestination(Target());

            return TaskStatus.Running;
        }

        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (target.Value != null)
            {
                return target.Value.transform.position;
            }
            return targetPosition.Value;
        }

        public override void OnReset()
        {
            base.OnReset();
            target = null;
            targetPosition = Vector3.zero;
        }
    }
}