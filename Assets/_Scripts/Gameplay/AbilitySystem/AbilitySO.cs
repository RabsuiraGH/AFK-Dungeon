using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem
{
    public abstract class AbilitySO : ScriptableObject, ILogableAbility
    {
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField] public Sprite Icon { get; protected  set; }

        [field: TextArea]
        [field: SerializeField] public string Description { get; protected  set; }

        [field: SerializeField] public BattleRole ApplyWhenRole { get; protected  set; }
        [field: SerializeField] public List<ConditionEntry> Conditions { get; protected  set; } = new();


        public void Log(string message, BattleContext context)
        {
            context.InnerLog($"{Name} applied. {message}");
        }


        public bool IsAllConditionsMet(BattleContext context)
        {
            bool roleMet = IsRoleMet(context);

            return roleMet && Conditions.All(entry => entry.Condition.IsMet(context, entry.Parameters));
        }


        protected bool IsRoleMet(BattleContext context)
        {
            return ApplyWhenRole switch
            {
                BattleRole.Attacker => context.Attacker == context.AbilityOwner,
                BattleRole.Defender => context.Defender == context.AbilityOwner,
                BattleRole.Any => true,
                _ => true
            };
        }
    }
}