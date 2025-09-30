using System.Collections.Generic;
using UnityEngine;

namespace LA.AudioSystem.Database
{
    [CreateAssetMenu(fileName = "MusicDatabase", menuName = "Game/Sound/MusicDatabase")]
    public class MusicDatabase : ScriptableObject
    {
        [field: SerializeField] public Music MenuMusic { get; private set; }
        [field: SerializeField] public Music GameMusic { get; private set; }
    }
}