using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "IncomingDamageMultAbility",
                     menuName = "Game/Abilities/Defensive/Incoming Damage Mult")]
    public class IncomingDamageMultAbility : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public int Multiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            if (IsAllConditionsMet(context))
            {
                context.AddDamageMultiplier(Multiplier);
                Log($"Owner: {context.AbilityOwner} Damage Multiplier: {Multiplier}");
            }
        }
    }
}