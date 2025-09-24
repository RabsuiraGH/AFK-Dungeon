using System;
using LA.Gameplay.Damage;
using UnityEngine;

namespace LA.Gameplay.WeaponSystem
{
    [Serializable]
    public struct Weapon
    {
        [field: SerializeField] public WeaponSO WeaponSource { get; private set; }

        [field: SerializeField] public int Damage { get; private set; }


        public Weapon(WeaponSO weaponSource)
        {
            WeaponSource = weaponSource;
            Damage = weaponSource != null ? weaponSource.BaseDamage : 0;
        }


        public DamageContext ToDamageContext() => new DamageContext(Damage, WeaponSource.DamageType, WeaponSource.Name);
    }
}