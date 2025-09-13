
using UnityEngine;

namespace LA.Gameplay.Abilities
{
    /// <summary>
    /// Base class for unity storage. Use  <see cref="LA.Gameplay.Abilities.AbilityCondition"/>
    /// to work with custom conditions.
    /// </summary>
    public abstract class AbilityConditionBase : ScriptableObject
    {
        public abstract bool IsMet(BattleContext context, object parameters);
    }

    /// <summary>
    /// Class to work with condition parameters.
    /// </summary>
    /// <typeparam name="TParams">Condition parameters to work with.</typeparam>
    public abstract class AbilityCondition<TParams> : AbilityConditionBase where TParams : ConditionParameters
    {
        public abstract bool IsMet(BattleContext context, TParams parameters);


        public override bool IsMet(BattleContext context, object parameters)
        {
            return IsMet(context, (TParams)parameters);
        }
    }
}