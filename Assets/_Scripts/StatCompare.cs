using System;
using UnityEngine;

namespace LA
{
    [Serializable]
    public struct StatCompare
    {
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public StatCompareType StatCompareType { get; private set; }


        public bool Compare(Stats left, Stats right)
        {
            int leftValue = left.GetStat(StatType);
            int rightValue = right.GetStat(StatType);


            return StatCompareType switch
            {
                StatCompareType.Equal => leftValue == rightValue,
                StatCompareType.LessThan => leftValue < rightValue,
                StatCompareType.MoreThan => leftValue > rightValue,
                StatCompareType.MoreThanOrEqual => leftValue >= rightValue,
                StatCompareType.LessThanOrEqual => leftValue <= rightValue,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum StatCompareType
    {
        Equal,
        MoreThan,
        LessThan,
        MoreThanOrEqual,
        LessThanOrEqual
    }
}