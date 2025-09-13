using LA.Abilities.Interfaces;
using UnityEngine;

namespace LA.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "DamageModTurnConditionAbility",
                     menuName = "Game/Abilities/Offensive/Damage Mod Turn Condition")]
    public class DamageModTurnConditionAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int Damage { get; protected set; }
        [field: SerializeField] public int OnTurn { get; protected set; }
        [field: SerializeField] public bool Repeat { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (!Repeat && context.TurnNumber == OnTurn)
            {
                context.AddDamage(new DamageContext(Damage, DamageType.None, Name));
                return;
            }

            if (context.TurnNumber % OnTurn != 0)
                return;


            context.AddDamage(new DamageContext(Damage, DamageType.None, Name));
        }
    }
}