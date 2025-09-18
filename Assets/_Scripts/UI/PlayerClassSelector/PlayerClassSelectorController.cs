using System.Collections.Generic;
using System.Linq;
using SW.Utilities.LoadAsset;
using UnityEngine;

namespace LA.UI
{
    public class PlayerClassSelectorController : MonoBehaviour
    {
        [SerializeField] private PlayerClassSelectorUI _playerClassSelectorUI;

        private Player _player;
        private ClassesDatabase _classesDatabase;
        private GameplayConfig _gameplayConfig;
        private MainGameLoop _mainGameLoop;


        [VContainer.Inject]
        public void Construct(Player player, MainGameLoop mainGameLoop, PathConfig pathConfig)
        {
            _classesDatabase = LoadAssetUtility.Load<ClassesDatabase>(pathConfig.ClassesDatabase);
            _gameplayConfig = LoadAssetUtility.Load<GameplayConfig>(pathConfig.GameplayConfig);

            _player = player;
            _mainGameLoop = mainGameLoop;

            _mainGameLoop.OnPlayerWin += OnPlayerWin;
        }


        private void Start()
        {
            List<PlayerClassData> availableClasses = GetAvailableClasses();

            _playerClassSelectorUI.PrepareUI(availableClasses, _player.TotalLevel);

            _playerClassSelectorUI.OnClassLevelAdded += LevelUpClass;
        }


        private void LevelUpClass(int classIndex)
        {
            _player.AddClass(_classesDatabase.Classes[classIndex]);
            _playerClassSelectorUI.Hide();
        }


        private void OnPlayerWin()
        {
            if (_player.TotalLevel < _gameplayConfig.MaxPlayerLevel)
            {
                List<PlayerClassData> availableClasses = GetAvailableClasses();
                _playerClassSelectorUI.UpdateUI(availableClasses, _player.TotalLevel);
                _playerClassSelectorUI.Show();
            }
            else
            {
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
            _mainGameLoop.OnPlayerWin -= OnPlayerWin;
        }
    }
}