using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace LA
{
    public class BattleContext
    {
        public int TurnNumber;
        public BattleUnit Attacker;
        public BattleUnit Defender;

        public BattleUnit AbilityOwner;

        public IReadOnlyDictionary<BattleUnit, int> TurnCounters;

        private Dictionary<BattleUnit, DamageContext> _weaponDamage;
        private Dictionary<BattleUnit, List<DamageContext>> _otherDamages;
        private Dictionary<BattleUnit, List<int>> _damageMultipliers;

        public bool IsHit;
        public int HitChance;


        public void Init()
        {
            _weaponDamage = new Dictionary<BattleUnit, DamageContext>
            {
                { Attacker, Attacker.CurrentWeapon.ToDamageContext() },
                { Defender, Defender.CurrentWeapon.ToDamageContext() }
            };
            _otherDamages = new Dictionary<BattleUnit, List<DamageContext>>
            {
                { Attacker, new List<DamageContext>() },
                { Defender, new List<DamageContext>() }
            };
            _damageMultipliers = new Dictionary<BattleUnit, List<int>>
            {
                { Attacker, new List<int>() },
                { Defender, new List<int>() }
            };
        }


        public int GetTurnCountFor(BattleUnit unit)
        {
            return TurnCounters.GetValueOrDefault(unit, 0);
        }


        public DamageContext GetWeaponDamage(BattleUnit weaponOwner = null)
        {
            return _weaponDamage[weaponOwner];
        }


        public void AddWeaponDamage(int bonusDamage, BattleUnit damageSource = null)
        {
            _weaponDamage[damageSource].ModifyDamage(bonusDamage);
        }


        public void AddDamage(DamageContext damageContext, BattleUnit damageSource = null)
        {
            _otherDamages[damageSource].Add(damageContext);
        }


        public void AddDamageMultiplier(int value, BattleUnit damageSource = null)
        {
            _damageMultipliers[damageSource].Add(value);
        }


        public int GetTotalDamage(BattleUnit damageSource)
        {
            int totalDamage = 0;

            totalDamage += _weaponDamage[damageSource].TotalDamage;
            totalDamage += _otherDamages[damageSource].Sum(x => x.TotalDamage);
            totalDamage += damageSource.Stats.Strength;

            totalDamage *= _damageMultipliers[damageSource].Aggregate(1, (x, y) => x * y);
            return totalDamage;
        }
    }
}