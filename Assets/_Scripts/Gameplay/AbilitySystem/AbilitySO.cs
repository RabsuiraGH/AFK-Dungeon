using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using UnityEngine;

namespace LA.Gameplay.AbilitySystem
{
    public abstract class AbilitySO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public List<ConditionEntry> Conditions { get; set; } = new();


        public bool IsAllConditionsMet(BattleContext context)
        {
            return Conditions.All(entry => entry.Condition.IsMet(context, entry.Parameters));
        }
    }
}