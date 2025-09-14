using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.Abilities;
using UnityEngine;

namespace LA.Gameplay.Abilities.Conditions
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

    public class StatConditionParametersBase : ConditionParametersBase
    {
        [field: SerializeField] public List<StatCompare> StatsToCompare { get; private set; } = new();
    }
}