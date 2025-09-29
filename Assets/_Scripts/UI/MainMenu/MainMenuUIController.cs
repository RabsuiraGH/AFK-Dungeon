using System;
using LA.UI.SettingsMenu;
using SW.Utilities.LoadAsset;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LA.UI.MainMenu
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private SettingsMenuUIController _settingsMenuUIController;

        private string _sceneName;

        [VContainer.Inject]
        public void Construct(PathConfig pathConfig)
        {
            _sceneName = pathConfig.GameScene.Split('/')[^1].Split('.')[0];
        }
        private void Start()
        {
            _mainMenuUI.OnStartButtonClicked += OnStartButtonClicked;
            _mainMenuUI.OnSettingsButtonClicked += OnSettingsButtonClicked;
            _mainMenuUI.OnQuitButtonClicked += OnQuitButtonClicked;
        }


        private void OnStartButtonClicked()
        {
            SceneManager.LoadScene(_sceneName);
        }


        private void OnSettingsButtonClicked()
        {
            _settingsMenuUIController.Show();
        }


        private void OnQuitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}