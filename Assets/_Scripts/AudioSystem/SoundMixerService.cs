using System;
using SW.Utilities.LoadAsset;
using UnityEngine;
using UnityEngine.Audio;
using VContainer.Unity;

namespace LA.AudioSystem
{
    public class SoundMixerService : IDisposable, IStartable
    {
        private AudioMixer _audioMixer;

        private const string MASTER_VOLUME_PARAMETER = "MasterVolume";
        private const string MUSIC_VOLUME_PARAMETER = "MusicVolume";
        private const string SFX_VOLUME_PARAMETER = "SFXVolume";

        private const float DEFAULT_MASTER_VOLUME = 1;
        private const float DEFAULT_SFX_VOLUME = 1;
        private const float DEFAULT_MUSIC_VOLUME = 0.5f;

        public event Action OnVolumeChanged;


        [VContainer.Inject]
        public void Construct(PathConfig pathConfig)
        {
            _audioMixer = LoadAssetUtility.Load<AudioMixer>(pathConfig.AudioMixer);
        }


        public void SetMasterVolume(float volume) => SetVolume(MASTER_VOLUME_PARAMETER, volume);

        public float GetMasterVolume() => GetVolume(MASTER_VOLUME_PARAMETER);

        public void SetMusicVolume(float volume) => SetVolume(MUSIC_VOLUME_PARAMETER, volume);

        public float GetMusicVolume() => GetVolume(MUSIC_VOLUME_PARAMETER);

        public void SetSFXVolume(float volume) => SetVolume(SFX_VOLUME_PARAMETER, volume);

        public float GetSFXVolume() => GetVolume(SFX_VOLUME_PARAMETER);


        private void SetVolume(string parameter, float volume)
        {
            _audioMixer.SetFloat(parameter, GetVolumeInDb(volume));
            OnVolumeChanged?.Invoke();
        }


        private float GetVolume(string parameter)
        {
            _audioMixer.GetFloat(parameter, out float volume);
            return GetVolumeFromDb(volume);
        }


        private float GetVolumeInDb(float volume)
        {
            return Mathf.Log10(volume) * 20;
        }


        private float GetVolumeFromDb(float volume)
        {
            return Mathf.Pow(10, volume / 20);
        }


        public void Dispose()
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_PARAMETER, GetMasterVolume());
            PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAMETER, GetMusicVolume());
            PlayerPrefs.SetFloat(SFX_VOLUME_PARAMETER, GetSFXVolume());
        }


        public void Start()
        {
            SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_PARAMETER, DEFAULT_MASTER_VOLUME));
            SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAMETER, DEFAULT_MUSIC_VOLUME));
            SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_PARAMETER, DEFAULT_SFX_VOLUME));
        }
    }
}