using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LA.UI
{
    public class PlayerClassSelectorController : MonoBehaviour
    {
        [SerializeField] private PlayerClassSelectorUI _playerClassSelectorUI;

        [SerializeField] private List<ClassSO> _availableClasses = new();

        [SerializeField] private Player _player;


        [VContainer.Inject]
        public void Construct(Player player)
        {
            _player = player;
        }


        private void Start()
        {
            _playerClassSelectorUI.PrepareUI(_availableClasses);

            _playerClassSelectorUI.OnClassLevelAdded += LevelUpClass;
        }


        private void LevelUpClass(int classIndex)
        {
            _player.AddClass(_availableClasses[classIndex]);
            UpdateClassesText();
        }


        private void UpdateClassesText()
        {
            StringBuilder sb = new();
            foreach (PlayerClassData classData in _player.ClassesData)
            {
                sb.Append(classData.Class.ClassName);
                sb.Append(" - ");
                sb.Append(classData.Level);
                sb.AppendLine();
            }

            _playerClassSelectorUI.UpdateCurrentClasses(sb.ToString());
        }
    }
}