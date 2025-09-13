using LA.Gameplay.Abilities.Interfaces;
using UnityEngine;

namespace LA.Gameplay.Abilities.Abilities
{
    [CreateAssetMenu(fileName = "New Stat Modifier Ability",
                     menuName = "Game/Abilities/Disposable/Stat Modifier")]
    public class StatModifierAbility : AbilitySO, IStatAbility
    {
        [field: SerializeField] public Stats Modifier { get; protected set; }


        public void Apply(BattleUnit unit)
        {
            unit.Stats.AddStats(Modifier);
        }


        public void Remove(BattleUnit unit)
        {
            unit.Stats.RemoveStats(Modifier);
        }
    }
}