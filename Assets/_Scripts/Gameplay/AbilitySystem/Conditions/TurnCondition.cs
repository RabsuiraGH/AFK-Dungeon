using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "TurnCondition", menuName = "Game/Abilities/Conditions/Turn Condition")]
    public class TurnCondition : AbilityCondition<TurnConditionParameters>
    {
        public override bool IsMet(BattleContext context, TurnConditionParameters parameters)
        {
            StringBuilder sb = new();
            int turn = parameters.UseGlobalTurn ? context.TurnNumber : context.GetTurnCountFor(context.AbilityOwner);

            sb.AppendLine($"Turn: {turn}");

            if (parameters.OnTurn.Contains(turn))
            {
                sb.AppendLine($"OnTurn: {turn}");
                Debug.Log(($"{sb.ToString()}"));
                return true;
            }

            if (parameters.RepeatEach && parameters.OnTurn.Any(x => turn % x == 0))
            {
                sb.AppendLine($"RepeatEach: {turn}");
                Debug.Log(($"{sb.ToString()}"));

                return true;
            }

            Debug.Log(($"{sb.ToString()}"));


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