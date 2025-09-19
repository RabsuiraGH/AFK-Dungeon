using System;
using UnityEngine;

namespace LA.UI
{
    public class GameUILinker : MonoBehaviour
    {
        [SerializeField] private StartBattleUIController _startBattleUIController;
        [SerializeField] private PlayerClassSelectorController _playerClassSelectorController;
        [SerializeField] private UnitInfoUIController _unitInfoUIController;

        [SerializeField] private MainLoopSim _mainLoopSim;
        [SerializeField] private Player _player;


        [VContainer.Inject]
        public void Construct(MainLoopSim mainLoopSim, Player player, PathConfig pathConfig)
        {
            _mainLoopSim = mainLoopSim;
            _player = player;
        }


        private void Start()
        {
            _playerClassSelectorController.OnClassSelected += InitPlayerUI;
            _playerClassSelectorController.MaxLevelReachedAlready += ShowStartBattleUI;
            _startBattleUIController.OnStartBattleRequested += _mainLoopSim.StartBattle;

            _mainLoopSim.MainGameLoop.OnEnemySet += _unitInfoUIController.SetEnemyInfo;

            _playerClassSelectorController.Setup();
        }


        private void InitPlayerUI()
        {
            _unitInfoUIController.SetPlayerInfo(_player);
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
            _playerClassSelectorController.OnClassSelected -= InitPlayerUI;
            _playerClassSelectorController.MaxLevelReachedAlready -= ShowStartBattleUI;
            _startBattleUIController.OnStartBattleRequested -= _mainLoopSim.StartBattle;
        }
    }
}