using System;
using SW.Utilities.LoadAsset;
using UnityEngine;

namespace LA.UI.BattleHeader
{
    public class BattleHeaderUIController : MonoBehaviour
    {
        [SerializeField] private BattleHeaderUI _battleHeaderUI;

        private GameService _gameService;
        private GameplayConfig _gameplayConfig;


        [VContainer.Inject]
        public void Construct(PathConfig pathConfig, GameService gameService)
        {
            _gameplayConfig = LoadAssetUtility.Load<GameplayConfig>(pathConfig.GameplayConfig);
            _gameService = gameService;

            _gameService.BattleService.OnTurnCountUpdated += _battleHeaderUI.SetTurn;

            _battleHeaderUI.OnSpeedButtonClicked += OnSpeedButtonClicked;
            _battleHeaderUI.OnPauseButtonClicked += OnPauseButtonClicked;
        }


        private void OnPauseButtonClicked()
        {
            _gameService.PauseBattle();
        }


        private void Start()
        {
            _battleHeaderUI.Setup(_gameplayConfig.GameSpeeds);
        }


        private void OnSpeedButtonClicked(int speedIndex)
        {
            _gameService.SetGameSpeed(_gameplayConfig.GameSpeeds[speedIndex]);
            _gameService.ResumeBattle();
        }


        private void OnDestroy()
        {
            _gameService.BattleService.OnTurnCountUpdated -= _battleHeaderUI.SetTurn;

            _battleHeaderUI.OnSpeedButtonClicked -= OnSpeedButtonClicked;
            _battleHeaderUI.OnPauseButtonClicked -= OnPauseButtonClicked;
        }
    }
}