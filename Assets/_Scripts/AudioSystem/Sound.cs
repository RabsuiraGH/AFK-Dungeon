using UnityEngine;

namespace LA.AudioSystem
{
    [System.Serializable]
    public class Sound
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public float Volume { get; private set; }
    }
}