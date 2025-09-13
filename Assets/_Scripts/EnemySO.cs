using System.Collections.Generic;
using LA.Abilities;
using LA.WeaponSystem;
using UnityEngine;

namespace LA
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Game/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField] public int BaseHealth { get; set; }
        [field: SerializeField] public WeaponSO BaseWeapon { get; set; }

        [field: SerializeField] public Stats BaseStats { get; set; }

        [field: SerializeField] public WeaponSO DeathDrop { get; set; }

        [field: SerializeField] public List<AbilitySO> Abilities { get; set; }
    }
}