using UnityEngine;

namespace LA.Gameplay.AbilitySystem
{
    /// <summary>
    /// Base class for unity storage. Use  <see cref="AbilityCondition{TParams}"/>
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
    public abstract class AbilityCondition<TParams> : AbilityConditionBase where TParams : ConditionParametersBase
    {
        public abstract bool IsMet(BattleContext context, TParams parameters);


        public override bool IsMet(BattleContext context, object parameters)
        {
            return IsMet(context, (TParams)parameters);
        }
    }
}