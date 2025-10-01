using System;
using System.Collections.Generic;
using LA.Gameplay.WeaponSystem;
using UnityEngine;

namespace LA.Gameplay.Player.ClassSystem
{
    [CreateAssetMenu(fileName = "Class", menuName = "Game/Class"), Serializable]
    public class ClassSO : ScriptableObject
    {
        [field: SerializeField] public string ClassName { get; private set; }

        [field: SerializeField] public Sprite ClassIcon { get; private set; }

        [field: SerializeField] public int HealthPerLevel { get; private set; }

        [field: SerializeField] public WeaponSO StartWeapon { get; private set; }

        [field: SerializeField] public List<LevelUpgrade> LevelUpgrades { get; private set; } = new();

        public int MaxLevel => LevelUpgrades.Count;
    }
}