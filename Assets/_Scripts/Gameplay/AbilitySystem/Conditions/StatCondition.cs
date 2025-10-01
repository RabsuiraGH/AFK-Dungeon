using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.Stat;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "StatCondition", menuName = "Game/Abilities/Conditions/Stat Condition")]
    public class StatCondition : AbilityCondition<StatConditionParametersBase>
    {
        public override bool IsMet(BattleContext context, StatConditionParametersBase parameters)
        {
            Stats left = context.AbilityOwner.Stats;
            Stats right = context.GetOpponent(context.AbilityOwner).Stats;

            return parameters.StatsToCompare.All(stCmp => stCmp.Compare(left, right));
        }
    }

    [Serializable]
    public class StatConditionParametersBase : ConditionParametersBase
    {
        [field: SerializeField] public List<StatCompare> StatsToCompare { get; private set; } = new();
    }
}