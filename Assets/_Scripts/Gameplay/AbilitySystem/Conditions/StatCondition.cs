using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "StatCondition", menuName = "Game/Abilities/Conditions/Stat Condition")]
    public class StatCondition : AbilityCondition<StatConditionParametersBase>
    {
        public override bool IsMet(BattleContext context, StatConditionParametersBase parameters)
        {
            return parameters.StatsToCompare.All(stCmp => stCmp.Compare(context.Attacker.Stats,
                                                                        context.Defender.Stats));
        }
    }

    [Serializable]
    public class StatConditionParametersBase : ConditionParametersBase
    {
        [field: SerializeField] public List<StatCompare> StatsToCompare { get; private set; } = new();
    }
}