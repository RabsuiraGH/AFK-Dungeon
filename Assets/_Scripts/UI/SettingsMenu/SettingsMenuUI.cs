using System;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.SettingsMenu
{
    public class SettingsMenuUI : TemplateUI
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Button _backButton;

        public event Action<float> OnMasterVolumeChanged;
        public event Action<float> OnSfxVolumeChanged;
        public event Action<float> OnMusicVolumeChanged;
        public event Action OnBackButtonClicked;

        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener((val) => OnMasterVolumeChanged?.Invoke(val));
            _sfxVolumeSlider.onValueChanged.AddListener((val) => OnSfxVolumeChanged?.Invoke(val));
            _musicVolumeSlider.onValueChanged.AddListener((val) => OnMusicVolumeChanged?.Invoke(val));
            _backButton.onClick.AddListener(() => OnBackButtonClicked?.Invoke());
        }


        private void OnDestroy()
        {
            _masterVolumeSlider.onValueChanged.RemoveAllListeners();
            _sfxVolumeSlider.onValueChanged.RemoveAllListeners();
            _musicVolumeSlider.onValueChanged.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }
    }
}