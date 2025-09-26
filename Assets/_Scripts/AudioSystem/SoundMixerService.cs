using SW.Utilities.LoadAsset;
using UnityEngine;
using UnityEngine.Audio;

namespace LA.AudioSystem
{
    public class SoundMixerService
    {
        private AudioMixer _audioMixer;

        private readonly string _masterVolumeParameter = "MasterVolume";
        private readonly string _musicVolumeParameter = "MusicVolume";
        private readonly string _sfxVolumeParameter = "SFXVolume";


        [VContainer.Inject]
        public void Construct(PathConfig pathConfig)
        {
            _audioMixer = LoadAssetUtility.Load<AudioMixer>(pathConfig.AudioMixer);
        }


        public void SetMasterVolume(float volume)
        {
            _audioMixer.SetFloat(_masterVolumeParameter, GetVolumeInDb(volume));
        }


        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(_musicVolumeParameter, GetVolumeInDb(volume));
        }


        public void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat(_sfxVolumeParameter, GetVolumeInDb(volume));
        }


        private float GetVolumeInDb(float volume)
        {
            return Mathf.Log10(volume) * 20;
        }
    }
}