using System.Collections.Generic;
using LA.Abilities;
using LA.WeaponSystem;
using UnityEngine;

namespace LA
{
    public abstract class BattleUnit
    {
        [field: SerializeField] public string Name { get; set; } = "Unit";
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public int CurrentHealth { get; set; }
        [field: SerializeField] public Weapon CurrentWeapon { get; set; }
        [field: SerializeField] public Stats Stats { get; set; } = new();

        [field: SerializeField] public List<AbilitySO> Abilities { get; protected set; } = new List<AbilitySO>();

        public void RestoreHealth()
        {
            CurrentHealth = MaxHealth;
        }


        public abstract void Init();
        public abstract bool IsHit(int hitValue);

        public abstract int GetDamage();

        public abstract void TakeDamage(int damage);

        public abstract bool IsDead();
    }
}