using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.Abilities.Interfaces;
using UnityEngine;

namespace LA.Gameplay.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "DamageRedStatConditionAbility",
                     menuName = "Game/Abilities/Defensive/Total Damage Reduction")]
    public class TotalDamageReductionAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int Reduction { get; protected set; }

        public void OnBeforeHit(BattleContext context)
        {
            if (!IsAllConditionsMet(context)) return;

            context.AddDamage(new DamageContext(-Reduction, DamageType.None, Name));
        }
    }
}