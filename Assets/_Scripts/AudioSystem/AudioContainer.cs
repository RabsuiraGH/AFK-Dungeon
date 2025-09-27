using System;
using UnityEngine;

namespace LA.AudioSystem
{
    public class AudioContainer : MonoBehaviour
    {
        public static AudioContainer Create()
        {
            GameObject go = new GameObject("AudioContainer");
            return go.AddComponent<AudioContainer>();
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}