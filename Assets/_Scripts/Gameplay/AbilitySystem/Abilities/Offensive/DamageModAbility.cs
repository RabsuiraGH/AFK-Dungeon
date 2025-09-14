using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "DamageModAbility",
                     menuName = "Game/Abilities/Offensive/Damage Modificator")]
    public class DamageModAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int DamageBonus { get; protected set; }
        [field: SerializeField] public DamageType DamageType { get; protected set; } = DamageType.None;


        public void OnBeforeHit(BattleContext context)
        {
            if (!IsAllConditionsMet(context)) return;

            DamageContext bonusDamage = new(DamageBonus, DamageType, Name);
            context.AddDamage(bonusDamage);

            Log($"Owner: {context.AbilityOwner} Damage: {bonusDamage.BaseDamage} Type: {bonusDamage.DamageType}");
        }
    }
}