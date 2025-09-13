using LA.Abilities.Interfaces;
using UnityEngine;

namespace LA.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "WeaponBaseDamageImmunityAbility",
                     menuName = "Game/Abilities/Defensive/Weapon Base Damage Immunity")]
    public class WeaponBaseDamageImmunityAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public DamageType DamageType { get; set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (context.WeaponDamage.DamageType == DamageType)
            {
                context.WeaponDamage.ModifyDamage(-context.WeaponDamage.BaseDamage);
            }
        }
    }
}