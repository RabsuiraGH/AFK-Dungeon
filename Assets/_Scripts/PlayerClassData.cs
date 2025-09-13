using System;
using UnityEngine;

namespace LA
{
    [Serializable]
    public struct PlayerClassData
    {
        [field: SerializeField] public ClassSO Class { get; set; }
        [field: SerializeField] public int Level { get; set; }


        public PlayerClassData(ClassSO newClass, int level)
        {
            Class = newClass;
            Level = level;
        }
        public PlayerClassData LevelUp() => new() { Class = Class, Level = Level + 1 };
    }
}