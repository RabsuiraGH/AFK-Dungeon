using System;
using System.Collections.Generic;
using LA.WeaponSystem;
using UnityEngine;

namespace LA
{
    [CreateAssetMenu(fileName = "Class", menuName = "Game/Class"), Serializable]
    public class ClassSO : ScriptableObject
    {
        [field: SerializeField] public string ClassName { get; set; }

        [field: SerializeField] public Sprite ClassIcon { get; set; }

        [field: SerializeField] public int HealthPerLevel { get; set; }

        [field: SerializeField] public WeaponSO StartWeapon { get; set; }

        [field: SerializeField] public List<LevelUpgrade> LevelUpgrades { get; set; } = new();

        public int MaxLevel => LevelUpgrades.Count;
    }
}