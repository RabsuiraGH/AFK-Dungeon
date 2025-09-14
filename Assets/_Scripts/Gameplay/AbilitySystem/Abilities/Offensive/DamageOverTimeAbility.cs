using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "DamageOverTimeAbility", menuName = "Game/Abilities/Offensive/Damage Over Time")]
    public class DamageOverTimeAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int BaseDamage { get; protected set; }

        [field: SerializeField] public int BonusDamagePerTurn { get; protected set; }

        [field: SerializeField] public int DurationInProcs { get; protected set; }

        [field: SerializeField] public bool ProcOnlyOnMyTurn { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            int proc = ProcOnlyOnMyTurn ? context.GetTurnCountFor(context.AbilityOwner) : context.TurnNumber;

            if (proc >= DurationInProcs)
                return;

            int damage = BaseDamage + (proc) * BonusDamagePerTurn;

            if (damage <= 0) return;

            DamageContext damageContext = new(damage, DamageType.None, Name + $"{context.TurnNumber}");
            context.AddDamage(damageContext);

            Log($"Owner: {context.AbilityOwner} Damage: {damageContext.BaseDamage} Type: {damageContext.DamageType} Proc: {proc}");

        }
    }
}