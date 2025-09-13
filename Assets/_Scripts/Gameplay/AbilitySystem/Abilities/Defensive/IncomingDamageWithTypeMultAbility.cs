using LA.Gameplay.Abilities.Interfaces;
using UnityEngine;

namespace LA.Gameplay.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "IncomingDamageMultFromWeaponAbility",
                     menuName = "Game/Abilities/Defensive/Incoming Damage With Type Mult")]
    public class IncomingDamageWithTypeMultAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public DamageType RequiredDamageType { get; protected set; }

        [field: SerializeField] public int Multiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (context.WeaponDamage.DamageType != RequiredDamageType) return;

            context.AddDamageMultiplier(Multiplier);
        }
    }
}