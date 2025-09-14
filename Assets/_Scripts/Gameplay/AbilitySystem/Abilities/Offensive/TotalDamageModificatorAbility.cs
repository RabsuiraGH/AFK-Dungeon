using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "DamageModStatConditionAbility",
                     menuName = "Game/Abilities/Offensive/Total Damage Modificator ")]
    public class TotalDamageModificatorAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int DamageBonus { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (!IsAllConditionsMet(context)) return;

            DamageContext bonusDamage = new(DamageBonus, DamageType.None, Name);
            context.AddDamage(bonusDamage);
        }
    }
}