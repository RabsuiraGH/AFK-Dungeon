using System;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.MainMenu
{
    public class MainMenuUI : TemplateUI
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        public event Action OnStartButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnQuitButtonClicked;


        private void Start()
        {
            _startButton.onClick.AddListener(() => OnStartButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
            _quitButton.onClick.AddListener(() => OnQuitButtonClicked?.Invoke());
        }


        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}