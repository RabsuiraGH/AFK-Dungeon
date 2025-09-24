using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.Config;
using LA.Gameplay.Player;
using LA.Gameplay.Player.ClassSystem;
using SW.Utilities.LoadAsset;
using UnityEngine;

namespace LA.UI.PlayerClassSelector
{
    public class PlayerClassSelectorController : MonoBehaviour
    {
        [SerializeField] private PlayerClassSelectorUI _playerClassSelectorUI;

        private Player _player;
        private ClassesDatabase _classesDatabase;
        private GameplayConfig _gameplayConfig;

        public event Action OnClassSelected;
        public event Action MaxLevelReachedAlready;


        [VContainer.Inject]
        public void Construct(Player player, PathConfig pathConfig)
        {
            _classesDatabase = LoadAssetUtility.Load<ClassesDatabase>(pathConfig.ClassesDatabase);
            _gameplayConfig = LoadAssetUtility.Load<GameplayConfig>(pathConfig.GameplayConfig);

            _player = player;
        }


        private void Start()
        {
            _playerClassSelectorUI.OnClassLevelAdded += LevelUpClass;
        }


        public void Setup()
        {
            List<PlayerClassData> availableClasses = GetAvailableClasses();

            _playerClassSelectorUI.PrepareUI(availableClasses, _player.TotalLevel);
            _playerClassSelectorUI.UpdateStats(_player.Stats);

            _playerClassSelectorUI.Show();
        }


        private void LevelUpClass(int classIndex)
        {
            _player.AddClass(_classesDatabase.Classes[classIndex]);
            OnClassSelected?.Invoke();
            _playerClassSelectorUI.Hide();
        }


        public void OnPlayerWin()
        {
            if (_player.TotalLevel < _gameplayConfig.MaxPlayerLevel)
            {
                List<PlayerClassData> availableClasses = GetAvailableClasses();
                _playerClassSelectorUI.UpdateUI(availableClasses, _player.TotalLevel);
                _playerClassSelectorUI.UpdateStats(_player.Stats);
                _playerClassSelectorUI.Show();
            }
            else
            {
                MaxLevelReachedAlready?.Invoke();
                _playerClassSelectorUI.Hide();
            }
        }


        private List<PlayerClassData> GetAvailableClasses()
        {
            List<PlayerClassData> leveledUpClasses = new List<PlayerClassData>();

            foreach (ClassSO classSo in _classesDatabase.Classes)
            {
                PlayerClassData existing = _player.ClassesData.FirstOrDefault(c => c.Class == classSo);

                if (existing.Class != null)
                {
                    leveledUpClasses.Add(existing.LevelUp());
                }
                else
                {
                    leveledUpClasses.Add(new PlayerClassData(classSo, 1));
                }
            }

            return leveledUpClasses;
        }


        private void OnDestroy()
        {
            _playerClassSelectorUI.OnClassLevelAdded -= LevelUpClass;
        }
    }
}