using LA.Gameplay.Abilities.Interfaces;
using UnityEngine;

namespace LA.Gameplay.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "DamageOverTimeAbility", menuName = "Game/Abilities/Offensive/Damage Over Time")]
    public class DamageOverTimeAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int FirstTurnDamage { get; protected set; }

        [field: SerializeField] public int BonusDamagePerTurn { get; protected set; }

        [field: SerializeField] public int DurationInProcs { get; protected set; }

        [field: SerializeField] public bool ProcOnlyOnMyTurn { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            int proc = ProcOnlyOnMyTurn ? (context.TurnNumber - 1) / 2 : context.TurnNumber;

            if (proc >= DurationInProcs)
                return;

            int damage = FirstTurnDamage + (proc) * BonusDamagePerTurn;

            if (damage <= 0) return;

            DamageContext damageContext = new(damage, DamageType.None, Name + $"{context.TurnNumber}");
            context.AddDamage(damageContext);
        }
    }
}