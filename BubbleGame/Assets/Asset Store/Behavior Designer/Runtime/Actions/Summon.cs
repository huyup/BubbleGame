using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Summon : Action
    {
        public List<EnemyType> EnemyTypes = new List<EnemyType>();

        public List<int> StartPosList = new List<int>();

        private SummonCtr summonCtr;
        public override TaskStatus OnUpdate()
        {
            if (!summonCtr)
                summonCtr=GetComponent<SummonCtr>();

            for (int i = 0; i < EnemyTypes.Count; i++)
            {
                summonCtr.Summon(EnemyTypes[i], StartPosList[i]);
            }

            return TaskStatus.Success;
        }
    }
}