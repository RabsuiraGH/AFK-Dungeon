using LA.Gameplay.AbilitySystem.Interfaces;
using LA.Gameplay.Damage;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "WeaponDamageMultAbility",
                     menuName = "Game/Abilities/Offensive/Weapon Damage Multiplier")]
    public class WeaponDamageMultAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int Multiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (!IsAllConditionsMet(context)) return;

            DamageContext damage = context.GetWeaponDamage(context.Attacker);

            int bonusDamage = damage.BaseDamage * (Multiplier - 1);
            damage.ModifyDamage(bonusDamage);

            Log($"Owner: {context.AbilityOwner} Bonus Damage: {bonusDamage}");
        }
    }
}