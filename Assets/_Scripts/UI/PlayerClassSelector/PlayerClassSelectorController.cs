using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.Player;
using LA.Gameplay.Player.ClassSystem;
using LA.Gameplay.Player.Config;
using SW.Utilities.LoadAsset;
using UnityEngine;

namespace LA.UI.PlayerClassSelector
{
    public class PlayerClassSelectorController : MonoBehaviour
    {
        [SerializeField] private PlayerClassSelectorUI _playerClassSelectorUI;

        private Player _player;
        private ClassesDatabase _classesDatabase;
        private PlayerConfig _playerConfig;

        public event Action OnClassSelected;
        public event Action MaxLevelReachedAlready;


        [VContainer.Inject]
        public void Construct(Player player, PathConfig pathConfig)
        {
            _classesDatabase = LoadAssetUtility.Load<ClassesDatabase>(pathConfig.ClassesDatabase);
            _playerConfig = LoadAssetUtility.Load<PlayerConfig>(pathConfig.PlayerConfig);

            _player = player;
        }


        private void Start() => _playerClassSelectorUI.OnClassLevelAdded += LevelUpClass;


        public void Show()
        {
            if (_player.TotalLevel < _playerConfig.MaxPlayerLevel)
            {
                List<PlayerClassData> availableClasses = GetAvailableClasses();
                _playerClassSelectorUI.PrepareUI(availableClasses, _player.TotalLevel);
                _playerClassSelectorUI.UpdateStats(_player.Stats);
                _playerClassSelectorUI.UpdateHealth(_player.MaxHealth);
                _playerClassSelectorUI.Show();
            }
            else
            {
                MaxLevelReachedAlready?.Invoke();
                _playerClassSelectorUI.Hide();
            }
        }


        private void LevelUpClass(int classIndex)
        {
            _player.AddClass(_classesDatabase.Classes[classIndex]);
            OnClassSelected?.Invoke();
            _playerClassSelectorUI.Hide();
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