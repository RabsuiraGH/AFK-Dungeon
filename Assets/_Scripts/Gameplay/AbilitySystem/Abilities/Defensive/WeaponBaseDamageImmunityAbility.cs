using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "WeaponBaseDamageImmunityAbility",
                     menuName = "Game/Abilities/Defensive/Weapon Base Damage Immunity")]
    public class WeaponBaseDamageImmunityAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        public void OnBeforeHit(BattleContext context)
        {
            if (IsAllConditionsMet(context))
            {
                DamageContext damage = context.GetWeaponDamage(context.Attacker);

                damage.ModifyDamage(-damage.BaseDamage);
                Log($"Owner: {context.AbilityOwner} Damage Immunity: {damage.BaseDamage}");
            }
        }
    }
}