using System;
using System.Collections;
using LA.Gameplay;
using LA.Gameplay.Enemy;
using LA.Gameplay.GameLoop;
using LA.Gameplay.Player;
using LA.UI.Loot;
using LA.UI.GameMenu;
using LA.UI.PlayerClassSelector;
using LA.UI.StartBattle;
using LA.UI.UnitInfo;
using UnityEngine;

namespace LA.UI
{
    public class GameUILinker : MonoBehaviour
    {
        [SerializeField] private StartBattleUIController _startBattleUIController;
        [SerializeField] private PlayerClassSelectorController _playerClassSelectorController;
        [SerializeField] private UnitInfoUIController _unitInfoUIController;
        [SerializeField] private LootUIController _lootUIController;

        [SerializeField] private GameMenuUIController _gameMenuUIController;

        [SerializeField] private BattleResultPopupUI _battleResultPopupUI;

        [SerializeField] private GameService _gameService;
        [SerializeField] private Player _player;


        [VContainer.Inject]
        public void Construct(GameService gameService, Player player, PathConfig pathConfig)
        {
            _gameService = gameService;
            _player = player;
        }


        private void Start()
        {
            _playerClassSelectorController.OnClassSelected += BeforeBattle;
            _playerClassSelectorController.MaxLevelReachedAlready += ShowStartBattleUI;
            _startBattleUIController.OnStartBattleRequested += _gameService.StartBattle;

            _gameService.OnPlayerWinBattle += OnPlayerWinBattleWrapper;
            _gameService.OnPlayerLoseBattle += OnPlayerLoseBattleWrapper;
            _gameService.OnEnemySet += BattleServiceOnOnEnemySet;

            _gameService.OnPlayerCompletedGame += OnPlayerCompletedGameWrapper;

            _lootUIController.OnChoiceMade += ShowClassSelector;


            _playerClassSelectorController.Setup();
        }


        private void BattleServiceOnOnEnemySet(BattleUnit enemy)
        {
            _unitInfoUIController.SetUnitInfo(enemy);
        }


        private void OnPlayerCompletedGameWrapper()
        {
            StartCoroutine(OnPlayerCompletedGame());
        }


        private IEnumerator OnPlayerCompletedGame()
        {
            yield return _battleResultPopupUI.ShowPopup("YOU COMPLETED THE GAME!", 0.2f, 0.5f, 3f);

            _gameMenuUIController.Show();
        }


        private void OnPlayerLoseBattleWrapper(Enemy killedBy)
        {
            StartCoroutine(OnPlayerLoseBattle());
        }


        private IEnumerator OnPlayerLoseBattle()
        {
            yield return _battleResultPopupUI.ShowPopup("YOU LOSE! :(", 0.2f, 0.5f, 1f);

            _gameMenuUIController.Show();
        }


        private void ShowClassSelector()
        {
            _playerClassSelectorController.OnPlayerWin();
            _unitInfoUIController.SetUnitInfo(_player);
        }


        private void OnPlayerWinBattleWrapper()
        {
            _unitInfoUIController.HideEnemyInfo();
            StartCoroutine(OnPlayerWinBattle());
        }


        private IEnumerator OnPlayerWinBattle()
        {
            yield return _battleResultPopupUI.ShowPopup("YOU WIN!", 0.2f, 0.5f, 1f);

            _lootUIController.OnPlayerWin(_gameService.BattleService.GetEnemy().EnemyBase.DeathDrop);
        }


        private void BeforeBattle()
        {
            _unitInfoUIController.SetUnitInfo(_player);
            _startBattleUIController.Show();
        }


        private void ShowStartBattleUI()
        {
            _startBattleUIController.Show();
        }


        public void Func()
        {
            _playerClassSelectorController.Setup();
        }


        // 0. Show menu
        // 1. Start button pressed
        // 2. if player level < max level, show player class selector, otherwise (3)
        // 2.1. When class selected first time, show unit info
        // 2.2. When class selected other times, update unit info
        // 3. Show start battle button
        // 4. Start battle button pressed
        // 5. Show enemy info
        // 6. Start battle
        // 7. If battle win, show win popup and goto (2)
        // 8. If battle lose, show lose popup and goto (0)


        private void OnDestroy()
        {
            _playerClassSelectorController.OnClassSelected -= BeforeBattle;
            _playerClassSelectorController.MaxLevelReachedAlready -= ShowStartBattleUI;
            _startBattleUIController.OnStartBattleRequested -= _gameService.StartBattle;

            _gameService.OnPlayerWinBattle -= OnPlayerWinBattleWrapper;
            _gameService.OnPlayerLoseBattle -= OnPlayerLoseBattleWrapper;
            _gameService.OnEnemySet -= BattleServiceOnOnEnemySet;

            _gameService.OnPlayerCompletedGame -= OnPlayerCompletedGameWrapper;

            _gameService.OnEnemySet -= _unitInfoUIController.SetUnitInfo;

            _lootUIController.OnChoiceMade -= ShowClassSelector;
        }
    }
}