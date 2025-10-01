using System;
using LA.Gameplay.GameStarter;
using LA.Utilities.SceneName;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LA.UI.GameMenu
{
    public class GameMenuUIController : MonoBehaviour
    {
        [SerializeField] private GameMenuUI _gameMenuUI;

        private GameStarterService _gameStarterService;

        private string _mainMenuSceneName;

        public event Action OnGameContinue;
        public event Action OnSettingsRequested;


        [VContainer.Inject]
        public void Construct(GameStarterService gameStarterService, PathConfig pathConfig)
        {
            _gameStarterService = gameStarterService;
            _mainMenuSceneName = SceneNameUtility.GetNameFromPath(pathConfig.MainMenuScene);

            _gameMenuUI.OnContinueButtonClicked += ContinueGame;
            _gameMenuUI.OnResetGameButtonClicked += _gameStarterService.Reset;
            _gameMenuUI.OnSettingsButtonClicked += RequestSettings;
            _gameMenuUI.OnQuitGameButtonClicked += BackToMainMenu;
        }


        public void ShowWithoutContinueButton()
        {
            _gameMenuUI.ToggleContinueButton(false);
            _gameMenuUI.Show();
        }


        public void Show() => _gameMenuUI.Show();


        public void Toggle()
        {
            if (_gameMenuUI.IsVisible)
            {
                _gameMenuUI.Hide();
                ContinueGame();
            }
            else
            {
                Show();
            }
        }


        private void ContinueGame()
        {
            OnGameContinue?.Invoke();
            _gameMenuUI.Hide();
        }


        private void RequestSettings()
        {
            OnSettingsRequested?.Invoke();
        }


        private void BackToMainMenu()
        {
            _gameStarterService.Unload();
            SceneManager.LoadScene(_mainMenuSceneName);
        }


        private void OnDestroy()
        {
            _gameMenuUI.OnContinueButtonClicked -= ContinueGame;
            _gameMenuUI.OnResetGameButtonClicked -= _gameStarterService.Reset;
            _gameMenuUI.OnSettingsButtonClicked -= RequestSettings;
            _gameMenuUI.OnQuitGameButtonClicked -= BackToMainMenu;
        }
    }
}