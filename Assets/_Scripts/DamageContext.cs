using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LA
{
    [Serializable]
    public class DamageContext
    {
        [field: SerializeField] public string DamageSource { get; protected set;} = string.Empty;
        [field: SerializeField] public int BaseDamage { get; protected set; }
        [field: SerializeField] public int ModifiedDamage { get; protected set; }
        [field: SerializeField] public DamageType DamageType { get; protected set; }

        public int TotalDamage => (BaseDamage + ModifiedDamage);


        public DamageContext(int baseDamage, DamageType damageType, string damageSource = "")
        {
            BaseDamage = baseDamage;
            DamageType = damageType;
            DamageSource = damageSource;
        }


        public void ModifyDamage(int value)
        {
            ModifiedDamage += value;
        }

    }
}