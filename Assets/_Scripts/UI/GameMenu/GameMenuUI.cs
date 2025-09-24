using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace LA.UI.GameMenu
{
    public class GameMenuUI : TemplateUI
    {
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitGameButton;

        public event Action OnResetGameButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnQuitGameButtonClicked;


        public void Start()
        {
            _resetGameButton.onClick.AddListener(() => OnResetGameButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
            _quitGameButton.onClick.AddListener(() => OnQuitGameButtonClicked?.Invoke());
        }


        private void OnDestroy()
        {
            _resetGameButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
        }
    }
}