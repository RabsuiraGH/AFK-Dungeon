using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem
{
    public abstract class AbilitySO : ScriptableObject, ILogableAbility
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public Sprite Icon { get; set; }

        [field: TextArea]
        [field: SerializeField] public string Description { get; set; }

        [field: SerializeField] public BattleRole ApplyWhenRole { get; set; }
        [field: SerializeField] public List<ConditionEntry> Conditions { get; set; } = new();


        public bool IsAllConditionsMet(BattleContext context)
        {
            bool roleMet = ApplyWhenRole switch
            {
                BattleRole.Attacker => context.Attacker == context.AbilityOwner,
                BattleRole.Defender => context.Defender == context.AbilityOwner,
                BattleRole.Any => true,
                _ => true
            };

            return roleMet && Conditions.All(entry => entry.Condition.IsMet(context, entry.Parameters));
        }



        public void Log(string message, BattleContext context)
        {
            context.InnerLog($"{Name} applied. {message}");
        }
    }
}