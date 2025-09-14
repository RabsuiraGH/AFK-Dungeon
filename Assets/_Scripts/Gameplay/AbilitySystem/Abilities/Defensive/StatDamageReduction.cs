using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "StatDamageReduction", menuName = "Game/Abilities/Defensive/Stat Damage Reduction")]
    public class StatDamageReduction : AbilitySO, IDefensiveAbility, IOnBeforeHitAbility
    {
        [field: SerializeField] public Stats ReductionStatMultiplier { get; protected set; }


        public void OnBeforeHit(BattleContext context)
        {
            Stats temp = new(context.Defender.Stats);
            temp.MultiplyStats(ReductionStatMultiplier);

            int damageReduction = temp.GetSumOfStats();

            context.AddDamage(new DamageContext(-damageReduction, DamageType.None, Name));

            Log($"Owner: {context.AbilityOwner} Damage Reduction: {damageReduction}");
        }
    }
}