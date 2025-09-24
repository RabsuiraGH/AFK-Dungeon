using System;
using LA.Gameplay.WeaponSystem;
using UnityEngine;

namespace LA.Gameplay.Enemy
{
    [Serializable]
    public class Enemy : BattleUnit
    {
        [field: SerializeField] public EnemySO EnemyBase { get; set; }


        public Enemy(EnemySO enemyBase)
        {
            EnemyBase = enemyBase;
        }
        public override void Init()
        {
            Name = EnemyBase.Name;
            Sprite = EnemyBase.Sprite;
            MaxHealth = EnemyBase.BaseHealth;
            Stats = EnemyBase.BaseStats;
            RestoreHealth();

            CurrentWeapon = new Weapon(EnemyBase.BaseWeapon);
            Abilities.AddRange(EnemyBase.Abilities);
        }


        public override bool IsHit(int hitValue)
        {
            return hitValue > EnemyBase.BaseStats.Agility;
        }


        public override int GetDamage()
        {
            return EnemyBase.BaseWeapon.BaseDamage;
        }
    }
}