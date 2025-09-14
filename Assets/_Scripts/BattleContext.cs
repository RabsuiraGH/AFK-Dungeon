using System.Collections.Generic;
using System.Linq;

namespace LA
{
    public class BattleContext
    {
        public int TurnNumber;
        public BattleUnit Attacker;
        public BattleUnit Defender;

        public BattleUnit AbilityOwner;

        public IReadOnlyDictionary<BattleUnit, int> TurnCounters;


        public DamageContext WeaponDamage;

        public readonly List<DamageContext> OtherDamages = new();
        public readonly List<int> DamageMultipliers = new();

        public bool IsHit;
        public int HitChance;

        public int GetTurnCountFor(BattleUnit unit)
        {
            return TurnCounters.GetValueOrDefault(unit, 0);
        }
        public void AddDamage(DamageContext damageContext)
        {
            OtherDamages.Add(damageContext);
        }


        public void AddDamageMultiplier(int value)
        {
            DamageMultipliers.Add(value);
        }


        public int GetTotalDamage()
        {
            int totalDamage = 0;

            totalDamage += WeaponDamage.TotalDamage;
            totalDamage += OtherDamages.Sum(x => x.TotalDamage);
            totalDamage += Attacker.Stats.Strength;

            totalDamage *= DamageMultipliers.Aggregate(1, (x, y) => x * y);
            return totalDamage;
        }
    }
}