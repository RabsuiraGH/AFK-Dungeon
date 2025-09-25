using InspectorPathField;
using UnityEngine;

namespace LA
{
    [CreateAssetMenu(fileName = "PathConfig", menuName = "Game/PathConfig")]
    public class PathConfig : ScriptableObject
    {
        [field: SerializeField, PathField] public string GameScope { get; private set; }
        [field: SerializeField, PathField] public string EnemyDatabase { get; private set; }

        [field: SerializeField, PathField] public string ClassesDatabase { get; private set; }

        [field: SerializeField, PathField] public string GameplayConfig { get; private set; }

        [field: SerializeField, PathField] public string GameScene { get; private set; }
        [field: SerializeField, PathField] public string MainMenuScene { get; private set; }

        [field: SerializeField, PathField] public string PopupUI { get; private set; }

    }
}