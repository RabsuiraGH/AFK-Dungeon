using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "TurnCondition", menuName = "Game/Abilities/Conditions/Turn Condition")]
    public class TurnCondition : AbilityCondition<TurnConditionParameters>
    {
        public override bool IsMet(BattleContext context, TurnConditionParameters parameters)
        {
            int turn = parameters.UseGlobalTurn ? context.TurnNumber : context.GetTurnCountFor(context.AbilityOwner);


            if (parameters.OnTurn.Contains(turn))
            {
                return true;
            }

            if (parameters.RepeatEach && parameters.OnTurn.Any(x => turn % x == 0))
            {
                return true;
            }


            return false;
        }
    }

    [Serializable]
    public class TurnConditionParameters : ConditionParametersBase
    {
        [field: SerializeField] public List<int> OnTurn { get; private set; } = new();

        [field: SerializeField, Tooltip("If true, condition will be met each OnTurn turn. " +
                                        "Otherwise, it will be met only once per OnTurn turn. ")]
        public bool RepeatEach { get; private set; }

        [field: SerializeField, Tooltip("If false, condition will count only ability owner turns.")]
        public bool UseGlobalTurn { get; set; }
    }
}