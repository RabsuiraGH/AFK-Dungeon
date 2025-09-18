using System;
using System.Collections.Generic;
using UnityEngine;

namespace LA
{
    [Serializable]
    public class Stats
    {
        [field: SerializeField] public int Strength { get; set; }
        [field: SerializeField] public int Agility { get; set; }
        [field: SerializeField] public int Endurance { get; set; }


        public Stats()
        {
        }


        public Stats(Stats stats)
        {
            Strength = stats.Strength;
            Agility = stats.Agility;
            Endurance = stats.Endurance;
        }


        public void AddStats(Stats stats)
        {
            Strength += stats.Strength;
            Agility += stats.Agility;
            Endurance += stats.Endurance;
        }


        public void RemoveStats(Stats stats)
        {
            Strength -= stats.Strength;
            Agility -= stats.Agility;
            Endurance -= stats.Endurance;
        }


        public void MultiplyStats(Stats stats)
        {
            Strength *= stats.Strength;
            Agility *= stats.Agility;
            Endurance *= stats.Endurance;
        }


        public int GetSumOfStats()
        {
            return Strength + Agility + Endurance;
        }


        public int GetStat(StatType statType)
        {
            return statType switch
            {
                StatType.Agility => Agility,
                StatType.Strength => Strength,
                StatType.Endurance => Endurance,
                _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
            };
        }


        public Dictionary<StatType, int> GetStats()
        {
            return new Dictionary<StatType, int>()
            {
                { StatType.Agility, Agility },
                { StatType.Strength, Strength },
                { StatType.Endurance, Endurance }
            };
        }
    }

    public enum StatType
    {
        Agility,
        Strength,
        Endurance
    }
}