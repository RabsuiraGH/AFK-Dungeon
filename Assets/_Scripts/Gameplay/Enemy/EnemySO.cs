using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.Stat;
using LA.Gameplay.WeaponSystem;
using UnityEngine;

namespace LA.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Game/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        [field: SerializeField] public int BaseHealth { get; private set; }
        [field: SerializeField] public WeaponSO BaseWeapon { get; private set; }

        [field: SerializeField] public Stats BaseStats { get; private set; }

        [field: SerializeField] public WeaponSO DeathDrop { get; private set; }

        [field: SerializeField] public List<AbilitySO> Abilities { get; private set; }
    }
}