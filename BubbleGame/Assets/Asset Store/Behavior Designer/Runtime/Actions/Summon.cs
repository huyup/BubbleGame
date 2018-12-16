using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Summon : Action
    {
        private SummonCtr summonCtr;
        public override TaskStatus OnUpdate()
        {
            if (!summonCtr)
                summonCtr=GetComponent<SummonCtr>();
            summonCtr.Summon(EnemyType.Uribou, 0);
            summonCtr.Summon(EnemyType.Uribou, 1);
            return TaskStatus.Success;
        }
    }
}