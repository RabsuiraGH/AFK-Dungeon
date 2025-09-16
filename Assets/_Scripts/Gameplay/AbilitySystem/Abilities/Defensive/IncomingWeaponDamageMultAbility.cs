using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "IncomingWeaponDamageMultAbility",
                     menuName = "Game/Abilities/Defensive/Incoming Weapon Damage Mult ")]
    public class IncomingWeaponDamageMultAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int Multiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (!IsAllConditionsMet(context)) return;

            int bonusDamage = context.GetWeaponDamage(context.Attacker).BaseDamage * (Multiplier - 1);
            context.GetWeaponDamage(context.Attacker).ModifyDamage(bonusDamage);
            Log($"Owner: {context.AbilityOwner} Bonus Damage: {bonusDamage}");
        }
    }
}