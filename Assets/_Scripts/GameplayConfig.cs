using UnityEngine;

namespace LA
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Game/Config/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxPlayerLevel { get; private set; }
    }
}