using System.Collections.Generic;
using System.Linq;
using LA.Abilities.Interfaces;
using UnityEngine;

namespace LA.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "DamageModStatConditionAbility",
                     menuName = "Game/Abilities/Offensive/Damage Mod Stat Condition")]
    public class DamageModStatConditionAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility, IConditionalAbility
    {
        [field: SerializeField] public List<StatCompare> StatsToCompare { get; protected set; } = new();
        [field: SerializeField] public int DamageBonus { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (!CheckCondition(context)) return;

            DamageContext stealthDamage = new(DamageBonus, DamageType.None, Name);
            context.AddDamage(stealthDamage);
        }


        public bool CheckCondition(BattleContext context)
        {
            return StatsToCompare.All(stCmp => stCmp.Compare(context.Attacker.Stats, context.Defender.Stats));
        }
    }
}