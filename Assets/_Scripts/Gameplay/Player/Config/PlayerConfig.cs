using InspectorPathField;
using LA.Gameplay.Stat;
using UnityEngine;

namespace LA.Gameplay.Player.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Player/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public string PlayerName { get; private set; } = "Player";
        [field: SerializeField, PathField] public string PlayerSprite { get; private set; }

        [field: SerializeField] public Stats MinimumStats { get; private set; }
        [field: SerializeField] public Stats MaximumStats { get; private set; }
        [field: SerializeField] public int MaxPlayerLevel { get; private set; } = 3;
    }
}