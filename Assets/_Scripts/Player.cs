using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.WeaponSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LA
{
    [Serializable]
    public class Player : BattleUnit
    {
        [field: SerializeField] public List<PlayerClassData> ClassesData { get; set; } = new();


        public override void Init()
        {
            Name = "Player";
            MaxHealth = 0;
            CurrentWeapon = new Weapon();
            Abilities = new List<AbilitySO>();

            Stats.Strength = Random.Range(1, 4);
            Stats.Agility = Random.Range(1, 4);
            Stats.Endurance = Random.Range(1, 4);

            RestoreHealth();
        }


        public void AddClass(ClassSO addedClass)
        {
            if (TryAddExistingClass(addedClass))
                return;

            AddNewClass(addedClass);
        }


        private void AddNewClass(ClassSO addedClass)
        {
            if (!ClassesData.Any() && CurrentWeapon.WeaponSource == null)
            {
                CurrentWeapon = new Weapon(addedClass.StartWeapon);
            }

            PlayerClassData newClassData = new(addedClass, 1);
            ApplyLevelUpgrade(newClassData, 1);
            ClassesData.Add(newClassData);
        }


        private bool TryAddExistingClass(ClassSO addedClass)
        {
            for (int i = 0; i < ClassesData.Count; i++)
            {
                if (ClassesData[i].Class != addedClass)
                    continue;

                ClassesData[i] = ClassesData[i].LevelUp();

                ApplyLevelUpgrade(ClassesData[i], ClassesData[i].Level);
                return true;
            }

            return false;
        }


        private void ApplyLevelUpgrade(PlayerClassData classData, int level)
        {
            if (level > classData.Class.LevelUpgrades.Count)
            {
                Debug.LogError(($"Level out of level upgrades range Class: {classData.Class} Level: {level}"));
                return;
            }

            classData.Class.LevelUpgrades[level - 1].ApplyToPlayer(this);

            MaxHealth += classData.Class.HealthPerLevel + Stats.Endurance;
        }


        public override bool IsHit(int hitValue)
        {
            return hitValue > Stats.Agility;
        }


        public override int GetDamage()
        {
            return CurrentWeapon.Damage + Stats.Strength;
        }
    }
}