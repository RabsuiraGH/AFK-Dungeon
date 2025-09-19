using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LA.UI.MainMenu
{
    public class GameMenuUIController : MonoBehaviour
    {
        [SerializeField] private GameMenuUI _gameMenuUI;

        private GameStarterService _gameStarterService;

        private string _mainMenuSceneName;


        [VContainer.Inject]
        public void Construct(GameStarterService gameStarterService, PathConfig pathConfig)
        {
            _gameStarterService = gameStarterService;
            _mainMenuSceneName = pathConfig.MainMenuScene.Split('/')[^1].Split('.')[0];

            _gameMenuUI.OnResetGameButtonClicked += _gameStarterService.Reset;
            //_gameMenuUI.OnSettingsButtonClicked +=
            _gameMenuUI.OnQuitGameButtonClicked += BackToMainMenu;
        }

        public void Show() => _gameMenuUI.Show();

        private void BackToMainMenu()
        {
            _gameStarterService.Unload();
            SceneManager.LoadScene(_mainMenuSceneName);
        }


        private void OnDestroy()
        {
            _gameMenuUI.OnResetGameButtonClicked -= _gameStarterService.Reset;
            //_gameMenuUI.OnSettingsButtonClicked -=
            _gameMenuUI.OnQuitGameButtonClicked -= BackToMainMenu;
        }
    }
}