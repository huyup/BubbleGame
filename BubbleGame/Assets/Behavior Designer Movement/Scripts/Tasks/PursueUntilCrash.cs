using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}PursueIcon.png")]
    public class PursueUntilCrash : NavMeshMovement
    {
        [Tooltip("How far to predict the distance ahead of the target. Lower values indicate less distance should be predicated")]
        public SharedFloat targetDistPrediction = 20;
        [Tooltip("Multiplier for predicting the look ahead distance")]
        public SharedFloat targetDistPredictionMult = 20;
        [Tooltip("The GameObject that the agent is pursuing")]
        public SharedGameObject target;

        [Tooltip("The GameObject that the agent is pursuing")]
        public SharedFloat DistanceToStop;

        // The position of the target at the last frame
        private Vector3 targetPosition;
        private bool hasArrived = false;
        private RaycastHit hit;
        public override void OnStart()
        {
            base.OnStart();


            targetPosition = target.Value.transform.position;
            Vector3 direction = (targetPosition - transform.position).normalized;
            targetPosition += direction*10;
            SetDestination(targetPosition);
        }

        // Pursue the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            ResetDestination();
            if (hasArrived)
            {

                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }

        private void ResetDestination()
        {
            int layerMask = 1 << 19;

            Vector3 direction = (targetPosition - transform.position).normalized;
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.distance < DistanceToStop.Value)
                {
                    hasArrived = true;
                }
            }
            else
            {
                targetPosition = target.Value.transform.position;
                hasArrived = true;
            }
        }
        public override void OnBehaviorRestart()
        {
            base.OnBehaviorRestart();
            hasArrived = false;
        }
        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();
            targetPosition = Vector3.zero;
            hasArrived = false;
            targetDistPrediction = 20;
            targetDistPredictionMult = 20;
            target = null;
        }
    }
}