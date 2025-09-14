using System;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "TurnCondition", menuName = "Game/Abilities/Conditions/Turn Condition")]
    public class DurationCondition : AbilityCondition<DurationConditionParameters>
    {
        public override bool IsMet(BattleContext context, DurationConditionParameters parameters)
        {
            int turns = parameters.UseGlobalTurn ? context.TurnNumber : context.GetTurnCountFor(context.AbilityOwner);

            return turns <= parameters.Duration;
        }
    }

    [Serializable]
    public class DurationConditionParameters : ConditionParametersBase
    {
        [field: SerializeField] public int Duration { get; private set; }

        [field: SerializeField, Tooltip("If false, condition will count only ability owner turns.")]
        public bool UseGlobalTurn { get; private set; }
    }
}