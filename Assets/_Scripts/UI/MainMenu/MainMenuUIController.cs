using System;
using LA.AudioSystem;
using LA.AudioSystem.Database;
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

        private MusicService _musicService;
        private MusicDatabase _musicDatabase;

        [VContainer.Inject]
        public void Construct(PathConfig pathConfig, MusicService musicService)
        {
            _musicService = musicService;
            _musicDatabase = LoadAssetUtility.Load<MusicDatabase>(pathConfig.MusicDatabase);
            _sceneName = pathConfig.GameScene.Split('/')[^1].Split('.')[0];
        }
        private void Start()
        {
            _musicService.PlayMusic(_musicDatabase.MenuMusic, Vector2.zero);


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