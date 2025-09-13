using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace LA.UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private Button _quitGameButton;
        private GameStarter _gameStarter;


        [VContainer.Inject]
        public void Construct(GameStarter gameStarter)
        {
            _gameStarter = gameStarter;

            _startGameButton.onClick.AddListener(() => _gameStarter.Load());
            _resetGameButton.onClick.AddListener(() => _gameStarter.Reset());
            _quitGameButton.onClick.AddListener(() => _gameStarter.Unload());
        }


        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _resetGameButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
        }
    }
}