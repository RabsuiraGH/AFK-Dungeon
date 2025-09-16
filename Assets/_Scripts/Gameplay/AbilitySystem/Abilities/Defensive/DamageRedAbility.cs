using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "DamageRedAbility",
                     menuName = "Game/Abilities/Defensive/Damage Reduction")]
    public class DamageRedAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int Reduction { get; protected set; }

        public void OnBeforeHit(BattleContext context)
        {
            if (!IsAllConditionsMet(context)) return;

            context.AddDamage(new DamageContext(-Reduction, DamageType.None, Name), context.Attacker);

            Log($"Owner: {context.AbilityOwner} Damage Reduction: {Reduction}");
        }
    }
}