using System.Collections;
using LA.Gameplay;
using LA.Gameplay.Enemy;
using LA.Gameplay.GameLoop;
using LA.Gameplay.Player;
using LA.UI.BattleHeader;
using LA.UI.Loot;
using LA.UI.GameMenu;
using LA.UI.PlayerClassSelector;
using LA.UI.SettingsMenu;
using LA.UI.StartBattle;
using LA.UI.UnitInfo;
using UnityEngine;

namespace LA.UI
{
    public class GameUILinker : MonoBehaviour
    {
        [SerializeField] private BattleHeaderUIController _battleHeaderUIController;
        [SerializeField] private StartBattleUIController _startBattleUIController;
        [SerializeField] private PlayerClassSelectorController _playerClassSelectorController;
        [SerializeField] private UnitInfoUIController _unitInfoUIController;
        [SerializeField] private LootUIController _lootUIController;

        [SerializeField] private GameMenuUIController _gameMenuUIController;
        [SerializeField] private SettingsMenuUIController _settingsMenuUIController;

        [SerializeField] private BattleResultPopupUI _battleResultPopupUI;

        [SerializeField] private GameService _gameService;
        [SerializeField] private Player _player;


        [VContainer.Inject]
        public void Construct(GameService gameService, Player player)
        {
            _gameService = gameService;
            _player = player;
        }


        private void Start()
        {
            _playerClassSelectorController.OnClassSelected += ShowStartBattleUI;
            _playerClassSelectorController.MaxLevelReachedAlready += ShowStartBattleUI;

            _startBattleUIController.OnStartBattleRequested += _gameService.StartBattle;

            _gameService.OnPlayerWinBattle += OnPlayerWinBattle;
            _gameService.OnPlayerLoseBattle += OnPlayerLoseBattle;
            _gameService.OnPlayerCompletedGame += OnPlayerCompletedGame;

            _gameService.OnEnemySet += BattleServiceOnOnEnemySet;


            _lootUIController.OnChoiceMade += ShowClassSelector;

            _gameMenuUIController.OnGameContinue += _gameService.ResumeBattle;
            _gameMenuUIController.OnSettingsRequested += ShowSettingsMenu;

            _battleHeaderUIController.OnMenuShowRequested += ToggleGameMenu;

            _playerClassSelectorController.Show();
        }


        private void ShowStartBattleUI()
        {
            _player.RestoreHealth();
            _unitInfoUIController.SetUnitInfo(_player);
            _startBattleUIController.Show();
        }


        private void ShowSettingsMenu()
        {
            _settingsMenuUIController.Show();
        }


        private void ToggleGameMenu()
        {
            _gameMenuUIController.Toggle();
            _settingsMenuUIController.Hide();
        }


        private void BattleServiceOnOnEnemySet(BattleUnit enemy)
        {
            _unitInfoUIController.SetUnitInfo(enemy);
        }


        private void ShowClassSelector()
        {
            _playerClassSelectorController.Show();
            _unitInfoUIController.SetUnitInfo(_player);
        }


        private void OnPlayerWinBattle(Enemy defeatedEnemy)
        {
            _unitInfoUIController.HideEnemyInfo();
            StartCoroutine(ProceedWin());

            IEnumerator ProceedWin()
            {
                yield return _battleResultPopupUI.ShowPopup("YOU WIN!", 0.2f, 0.5f, 1f);

                _lootUIController.OnPlayerWin(defeatedEnemy.EnemyBase.DeathDrop);
            }
        }


        private void OnPlayerLoseBattle(Enemy killedBy)
        {
            StartCoroutine(ProceedLose());

            IEnumerator ProceedLose()
            {
                yield return _battleResultPopupUI.ShowPopup("YOU LOSE!", 0.2f, 0.5f, 1f);

                _gameMenuUIController.ShowWithoutContinueButton();
            }
        }


        private void OnPlayerCompletedGame()
        {
            StartCoroutine(ProceedGameCompletion());

            IEnumerator ProceedGameCompletion()
            {
                yield return _battleResultPopupUI.ShowPopup("YOU COMPLETED THE GAME!", 0.2f, 0.5f, 3f);

                _gameMenuUIController.ShowWithoutContinueButton();
            }
        }


        private void OnDestroy()
        {
            _playerClassSelectorController.OnClassSelected -= ShowStartBattleUI;
            _playerClassSelectorController.MaxLevelReachedAlready -= ShowStartBattleUI;

            _startBattleUIController.OnStartBattleRequested -= _gameService.StartBattle;

            _gameService.OnPlayerWinBattle -= OnPlayerWinBattle;
            _gameService.OnPlayerLoseBattle -= OnPlayerLoseBattle;
            _gameService.OnEnemySet -= BattleServiceOnOnEnemySet;

            _gameService.OnPlayerCompletedGame -= OnPlayerCompletedGame;

            _gameService.OnEnemySet -= _unitInfoUIController.SetUnitInfo;

            _lootUIController.OnChoiceMade -= ShowClassSelector;
            _gameMenuUIController.OnGameContinue -= _gameService.ResumeBattle;
            _battleHeaderUIController.OnMenuShowRequested -= ToggleGameMenu;
        }
    }
}