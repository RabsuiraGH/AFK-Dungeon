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
        }


        private void Start()
        {
            _battleHeaderUI.Setup(_gameplayConfig.GameSpeeds);
        }


        private void OnSpeedButtonClicked(int speedIndex)
        {
            _gameService.SetGameSpeed(_gameplayConfig.GameSpeeds[speedIndex]);
        }


        private void OnDestroy()
        {
            _gameService.BattleService.OnTurnCountUpdated -= _battleHeaderUI.SetTurn;

            _battleHeaderUI.OnSpeedButtonClicked -= OnSpeedButtonClicked;
        }
    }
}