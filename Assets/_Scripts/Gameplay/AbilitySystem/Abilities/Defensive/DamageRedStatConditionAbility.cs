using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.Abilities.Interfaces;
using UnityEngine;

namespace LA.Gameplay.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "DamageRedStatConditionAbility",
                     menuName = "Game/Abilities/Defensive/Damage Red Stat Condition")]
    public class DamageRedStatConditionAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public List<StatCompare> StatsToCompare { get; protected set; } = new();
        [field: SerializeField] public int Reduction { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (!CheckCondition(context)) return;

            context.AddDamage(new DamageContext(-Reduction, DamageType.None, Name));
        }


        public bool CheckCondition(BattleContext context)
        {
            return StatsToCompare.All(stCmp => stCmp.Compare(context.Defender.Stats, context.Attacker.Stats));
        }
    }
}