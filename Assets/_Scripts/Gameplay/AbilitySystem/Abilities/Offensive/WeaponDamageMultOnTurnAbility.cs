using LA.Gameplay.Abilities.Interfaces;
using UnityEngine;

namespace LA.Gameplay.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "WeaponDamageMultOnTurnAbility",
                     menuName = "Game/Abilities/Offensive/Weapon Damage Mult On Turn")]
    public class WeaponDamageMultOnTurnAbility : AbilitySO, IOffensiveAbility, IOnBeforeHitAbility, IConditionalAbility
    {
        [field: SerializeField] public int ActivateOnTurn { get; protected set; }
        [field: SerializeField] public int Multiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if(!CheckCondition(context)) return;

            context.WeaponDamage.ModifyDamage(+context.WeaponDamage.BaseDamage * (Multiplier - 1));
        }


        public bool CheckCondition(BattleContext context)
        {
            return context.TurnNumber == ActivateOnTurn;
        }
    }
}