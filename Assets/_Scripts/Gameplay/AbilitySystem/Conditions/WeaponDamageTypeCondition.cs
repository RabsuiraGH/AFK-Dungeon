using UnityEngine;

namespace LA.Gameplay.AbilitySystem.Conditions
{
    [CreateAssetMenu(fileName = "WeaponDamageTypeCondition",
                     menuName = "Game/Abilities/Conditions/Weapon Damage Type Condition")]
    public class WeaponDamageTypeCondition : AbilityCondition<WeaponDamageTypeConditionParameters>
    {
        public override bool IsMet(BattleContext context, WeaponDamageTypeConditionParameters parameters)
        {
            return context.WeaponDamage.DamageType == parameters.DamageType;
        }
    }

    [System.Serializable]
    public class WeaponDamageTypeConditionParameters : ConditionParametersBase
    {
        [field: SerializeField] public DamageType DamageType { get; private set; }
    }
}