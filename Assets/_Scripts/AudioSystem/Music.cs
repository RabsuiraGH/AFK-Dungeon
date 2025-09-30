using UnityEngine;

namespace LA.AudioSystem
{
    [System.Serializable]
    public class Music : Sound
    {
        [field: SerializeField] public bool Loop { get; private set; }
    }
}