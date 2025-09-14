using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "IncomingDamageMultFromWeaponAbility",
                     menuName = "Game/Abilities/Defensive/Incoming Damage Mult From Weapon")]
    public class IncomingDamageMultFromWeaponAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public DamageType RequiredDamageType { get; protected set; }

        [field: SerializeField] public int Multiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (context.WeaponDamage.DamageType != RequiredDamageType) return;

            context.WeaponDamage.ModifyDamage(+context.WeaponDamage.BaseDamage * (Multiplier - 1));
        }
    }
}