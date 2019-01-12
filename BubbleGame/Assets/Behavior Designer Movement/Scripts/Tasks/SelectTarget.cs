using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class SelectTarget : Conditional
    {
        //public SharedGameObject player1;
        //public SharedGameObject player2;

        public SharedGameObject target;
        public SharedGameObject returnTarget;

        public override void OnStart()
        {
            base.OnStart();
        }
        

        public override TaskStatus OnUpdate()
        {

            if (returnTarget.Value.GetComponent<PlayerStatus>().nowHp > 0)
            {
                returnTarget.Value = target.Value;
                return TaskStatus.Success;
            }
            else
            {
                returnTarget.Value = target.Value;
                return TaskStatus.Failure;
            }

        }
    }
}