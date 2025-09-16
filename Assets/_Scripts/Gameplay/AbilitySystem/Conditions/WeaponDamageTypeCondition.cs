using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "WeaponDamageTypeCondition",
                     menuName = "Game/Abilities/Conditions/Weapon Damage Type Condition")]
    public class WeaponDamageTypeCondition : AbilityCondition<WeaponDamageTypeConditionParameters>
    {
        public override bool IsMet(BattleContext context, WeaponDamageTypeConditionParameters parameters)
        {
            if (parameters.DamageSource == DamageSource.Attacker)
            {
                return context.GetWeaponDamage(context.Attacker).DamageType == parameters.DamageType;
            }
            else
            {
                return context.GetWeaponDamage(context.Defender).DamageType == parameters.DamageType;
            }
        }
    }

    [System.Serializable]
    public class WeaponDamageTypeConditionParameters : ConditionParametersBase
    {
        [field: SerializeField] public DamageType DamageType { get; private set; }
        [field: SerializeField] public DamageSource DamageSource { get; private set; }
    }
}