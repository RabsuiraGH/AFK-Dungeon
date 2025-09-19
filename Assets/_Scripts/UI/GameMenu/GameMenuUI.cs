using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace LA.UI.MainMenu
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private Button _quitGameButton;
        private GameStarterService _gameStarterService;


        [VContainer.Inject]
        public void Construct(GameStarterService gameStarterService)
        {
            _gameStarterService = gameStarterService;

            _startGameButton.onClick.AddListener(() => _gameStarterService.Load());
            _resetGameButton.onClick.AddListener(() => _gameStarterService.Reset());
            _quitGameButton.onClick.AddListener(() => _gameStarterService.Unload());
        }


        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _resetGameButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
        }
    }
}