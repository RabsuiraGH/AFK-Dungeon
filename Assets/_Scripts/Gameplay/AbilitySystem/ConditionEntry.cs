using System;
using UnityEngine;

namespace LA.Gameplay.Abilities
{
    [Serializable]
    public sealed class ConditionEntry
    {
        public AbilityConditionBase Condition => _condition;
        public object Parameters => _parameters;

        [SerializeField] private AbilityConditionBase _condition;
        [SerializeReference] private object _parameters;
    }
}