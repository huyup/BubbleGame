using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class RotateTowardByTime : Action
    {
        public SharedFloat rotationEpsilon = 0.5f;
        public SharedFloat maxLookAtRotationDelta = 1;

        [Tooltip("The amount of time to wait")]
        public SharedFloat waitTime = 1;

        public SharedBool onlyY;

        [Tooltip("The GameObject that the agent is rotating towards")]
        public SharedGameObject target;

        [Tooltip("If target is null then use the target rotation")]
        public SharedVector3 targetRotation;

        public SharedFloat RotateSpeed;

        // The time to wait
        private float waitDuration;
        // The time that the task started to wait.
        private float startTime;
        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
        private float pauseTime;

        public override void OnStart()
        {
            // Remember the start time.
            startTime = Time.time;
            waitDuration = waitTime.Value;
        }
        public override TaskStatus OnUpdate()
        {
            // The task is done waiting if the time waitDuration has elapsed since the task was started.
            if (startTime + waitDuration < Time.time)
            {
                return TaskStatus.Success;
            }
            var rotation = Target();
            // Return a task status of success once we are done rotating
            if (Quaternion.Angle(transform.rotation, rotation) < rotationEpsilon.Value)
            {
                return TaskStatus.Success;
            }
            // We haven't reached the target yet so keep rotating towards it
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, maxLookAtRotationDelta.Value);
            return TaskStatus.Running;
        }
        public override void OnPause(bool paused)
        {
            if (paused)
            {
                // Remember the time that the behavior was paused.
                pauseTime = Time.time;
            }
            else
            {
                // Add the difference between Time.time and pauseTime to figure out a new start time.
                startTime += (Time.time - pauseTime);
            }
        }
        // Return targetPosition if targetTransform is null
        private Quaternion Target()
        {
            if (target == null || target.Value == null)
            {
                return Quaternion.Euler(targetRotation.Value);
            }
            var position = target.Value.transform.position - transform.position;
            if (onlyY.Value)
            {
                position.y = 0;
            }
            return Quaternion.LookRotation(position);
        }

        // Reset the public variables
        public override void OnReset()
        {
            // Reset the public properties back to their original values
            waitTime = 1;
            rotationEpsilon = 0.5f;
            maxLookAtRotationDelta = 1f;
            onlyY = false;
            target = null;
            targetRotation = Vector3.zero;
        }
    }
}