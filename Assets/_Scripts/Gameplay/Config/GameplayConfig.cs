using System.Collections.Generic;
using UnityEngine;

namespace LA.Gameplay.Config
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Game/Config/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public float BaseTurnIntervalInSeconds { get; private set; }
        [field: SerializeField] public List<float> GameSpeeds { get; private set; } = new() { 1f,2f };

    }
}