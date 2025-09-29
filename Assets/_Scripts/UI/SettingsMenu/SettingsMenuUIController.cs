using System;
using LA.AudioSystem;
using UnityEngine;

namespace LA.UI.SettingsMenu
{
    public class SettingsMenuUIController : MonoBehaviour
    {
        [SerializeField] private SettingsMenuUI _settingsMenuUI;

        private SoundMixerService _soundMixerService;


        [VContainer.Inject]
        public void Construct(SoundMixerService soundMixerService)
        {
            _soundMixerService = soundMixerService;
            _soundMixerService.OnVolumeChanged += SetVolumeSliders;


            _settingsMenuUI.OnMasterVolumeChanged += _soundMixerService.SetMasterVolume;
            _settingsMenuUI.OnSfxVolumeChanged += _soundMixerService.SetSFXVolume;
            _settingsMenuUI.OnMusicVolumeChanged += _soundMixerService.SetMusicVolume;
            _settingsMenuUI.OnBackButtonClicked += Hide;
        }


        private void Start()
        {
            SetVolumeSliders();
        }


        private void SetVolumeSliders()
        {
            _settingsMenuUI.SetMasterVolumeSlider(_soundMixerService.GetMasterVolume());
            _settingsMenuUI.SetSFXVolumeSlider(_soundMixerService.GetSFXVolume());
            _settingsMenuUI.SetMusicVolumeSlider(_soundMixerService.GetMusicVolume());
        }


        public void Show() => _settingsMenuUI.Show();

        public void Hide() => _settingsMenuUI.Hide();


        private void OnDestroy()
        {
            _soundMixerService.OnVolumeChanged += SetVolumeSliders;

            _settingsMenuUI.OnMasterVolumeChanged -= _soundMixerService.SetMasterVolume;
            _settingsMenuUI.OnSfxVolumeChanged -= _soundMixerService.SetSFXVolume;
            _settingsMenuUI.OnMusicVolumeChanged -= _soundMixerService.SetMusicVolume;
            _settingsMenuUI.OnBackButtonClicked -= Hide;
        }
    }
}