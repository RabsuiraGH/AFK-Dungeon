using System.Collections.Generic;
using UnityEngine;

namespace LA.AudioSystem
{
    [CreateAssetMenu(fileName = "SoundFXDatabase", menuName = "Game/Sound/SoundFXDatabase")]
    public class SoundFXDatabase : ScriptableObject
    {
        [field: SerializeField] public List<Sound> Attack { get; private set; }


        [field: SerializeField] public List<Sound> Extras { get; private set; }
    }
}