using LA.Gameplay.AbilitySystem.Interfaces;
using LA.Gameplay.Damage;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "DamageModWithPenaltyAbility",
                     menuName = "Game/Abilities/Offensive/Damage Mod With Penalty")]
    public class DamageModWithPenaltyAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int BonusDamage { get; protected set; }

        [field: SerializeField, Tooltip("Applied when the conditions are not met")]
        public int DamagePenalty { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (IsAllConditionsMet(context))
            {
                context.AddDamage(new DamageContext(BonusDamage, DamageType.None, Name + " Bonus"));
                Log($"Owner: {context.AbilityOwner} Bonus Damage: {BonusDamage}", context);
            }
            else if(IsRoleMet(context))
            {
                context.AddDamage(new DamageContext(DamagePenalty, DamageType.None, Name + " Penalty"));
                Log($"Owner: {context.AbilityOwner} Penalty Damage: {DamagePenalty}", context);
            }
        }
    }
}