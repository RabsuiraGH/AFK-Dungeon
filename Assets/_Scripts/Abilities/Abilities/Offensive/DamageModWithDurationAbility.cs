using LA.Abilities.Interfaces;
using UnityEngine;

namespace LA.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "DamageModWithDurationAbility",
                     menuName = "Game/Abilities/Offensive/Damage Mod With Duration")]
    public class DamageModWithDurationAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int BonusDamage { get; protected set; }
        [field: SerializeField] public int BonusDamageDuration { get; protected set; }

        [field: SerializeField] public int DamagePenaltyAfterDuration { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (context.TurnNumber <= BonusDamageDuration)
            {
                context.AddDamage(new DamageContext(BonusDamage, DamageType.None, Name + " Bonus"));
            }
            else
            {
                context.AddDamage(new DamageContext(DamagePenaltyAfterDuration, DamageType.None, Name + " Penalty"));
            }
        }
    }
}