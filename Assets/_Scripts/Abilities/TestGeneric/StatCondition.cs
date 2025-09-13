using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LA.Abilities.TestGeneric
{

    public struct StatConditionParams
    {
        [field: SerializeField] public List<StatCompare> StatsToCompare;

    }
    [CreateAssetMenu(menuName = "Abilities/Conditions/Stat Condition")]
    public class StatCondition : AbilityCondition<StatConditionParams>
    {
        public override bool IsMet(BattleContext context, StatConditionParams p)
        {
            return p.StatsToCompare.All(stCmp => stCmp.Compare(context.Defender.Stats, context.Attacker.Stats));
        }
    }
}