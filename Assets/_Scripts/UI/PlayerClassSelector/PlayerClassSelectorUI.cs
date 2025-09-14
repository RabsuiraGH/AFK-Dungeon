using System;
using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI
{
    public class PlayerClassSelectorUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentClasses;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private Button _addClassLevelButton;

        public event Action<int> OnClassLevelAdded;


        public void PrepareUI(List<ClassSO> availableClasses)
        {
            _currentClasses.text = string.Empty;
            _dropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> data = new();

            foreach (ClassSO avClass in availableClasses)
            {
                data.Add(new TMP_Dropdown.OptionData(avClass.ClassName));
            }

            _dropdown.AddOptions(data);

            _dropdown.value = 0;

            _addClassLevelButton.onClick.AddListener(() => OnClassLevelAdded?.Invoke(_dropdown.value));
        }


        public void UpdateCurrentClasses(string currentClasses) => _currentClasses.text = currentClasses;


        private void OnDestroy()
        {
            _addClassLevelButton.onClick.RemoveAllListeners();
        }
    }
}