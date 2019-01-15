using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class SelectTarget : Conditional
    {
        public SharedGameObject player1;
        public SharedGameObject player2;

        public SharedGameObject returnTarget;

        private GameObject target;

        public override void OnStart()
        {
            base.OnStart();
        }
        public override TaskStatus OnUpdate()
        {
            var hateValueCtr = GetComponent<BossHateValueCtr>();
            target = hateValueCtr.SendTargetToBehavior();

            if (!target)
            {
                Debug.Log("Cant Find target");
                return TaskStatus.Failure;
            }
            else
            {
                if (target.GetComponent<PlayerStatus>())
                {
                    if (target.GetComponent<PlayerStatus>().nowHp > 0)
                    {

                        returnTarget.Value = target;

                    }
                    else
                    {
                        if (target.GetComponent<PlayerStatus>().PlayerSelection == PlayerSelection.Player1)
                        {
                            returnTarget.Value = player2.Value;
                        }
                        else if (target.GetComponent<PlayerStatus>().PlayerSelection == PlayerSelection.Player2)
                        {
                            returnTarget.Value = player1.Value;
                        }
                    }
                }
                else
                {
                    returnTarget.Value = target;
                }

                return TaskStatus.Success;
            }
        }
    }
}