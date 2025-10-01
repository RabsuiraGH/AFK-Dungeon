using System;
using UnityEngine;

namespace LA.Gameplay.Player.ClassSystem
{
    [Serializable]
    public struct PlayerClassData
    {
        [field: SerializeField] public ClassSO Class { get; private set; }
        [field: SerializeField] public int Level { get; private set; }


        public PlayerClassData(ClassSO newClass, int level)
        {
            Class = newClass;
            Level = level;
        }
        public PlayerClassData LevelUp() => new() { Class = Class, Level = Level + 1 };
    }
}