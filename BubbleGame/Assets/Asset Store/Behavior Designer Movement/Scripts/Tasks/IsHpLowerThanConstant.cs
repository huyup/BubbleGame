using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class IsHpLowerThanConstant : Conditional
    {
        public SharedInt Hp;

        public SharedInt NextAttackHp;
        public override TaskStatus OnUpdate()
        {
            if (Hp.Value <= NextAttackHp.Value)
                return TaskStatus.Success;
            else
                return TaskStatus.Failure;
        }

    }
}