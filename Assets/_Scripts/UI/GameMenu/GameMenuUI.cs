using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace LA.UI.GameMenu
{
    public class GameMenuUI : TemplateUI
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitGameButton;

        public event Action OnContinueButtonClicked;
        public event Action OnResetGameButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnQuitGameButtonClicked;


        public void ToggleContinueButton(bool toggle) => _continueButton.gameObject.SetActive(toggle);

        public void Start()
        {
            _continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
            _resetGameButton.onClick.AddListener(() => OnResetGameButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
            _quitGameButton.onClick.AddListener(() => OnQuitGameButtonClicked?.Invoke());
        }


        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _resetGameButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
        }
    }
}