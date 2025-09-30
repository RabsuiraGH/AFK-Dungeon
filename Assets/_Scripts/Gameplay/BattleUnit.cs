using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.Stat;
using LA.Gameplay.WeaponSystem;
using UnityEngine;

namespace LA.Gameplay
{
    public abstract class BattleUnit
    {
        [field: SerializeField] public string Name { get; set; } = "Unit";
        [field: SerializeField] public Sprite Sprite { get; set; }
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public int CurrentHealth { get; set; }
        [field: SerializeField] public Weapon CurrentWeapon { get; set; }
        [field: SerializeField] public Stats Stats { get; set; } = new();

        [field: SerializeField] public List<AbilitySO> Abilities { get; protected set; } = new List<AbilitySO>();


        public void RestoreHealth()
        {
            CurrentHealth = MaxHealth;
        }


        override public string ToString()
        {
            return Name;
        }


        public abstract void Init();

        public abstract bool IsHit(int hitValue);

        public abstract int GetDamage();


        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }


        public virtual bool IsDead()
        {
            return CurrentHealth <= 0;
        }
    }
}