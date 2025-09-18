using System;
using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI
{
    public class PlayerClassSelectorUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Transform _contentPanel;
        [SerializeField] private List<ClassCard> _classCards;

        [SerializeField] private ClassCard _classCardPrefab;
        public event Action<int> OnClassLevelAdded;


        public void PrepareUI(IReadOnlyList<PlayerClassData> availableClasses, int totalLevel)
        {
            _levelText.text = $"Level: {totalLevel + 1}";

            foreach (PlayerClassData classData in availableClasses)
            {
                ClassCard card = Instantiate(_classCardPrefab, _contentPanel);

                card.SetData(classData, totalLevel);

                card.OnClick += AddClassLevelButtonPressed;
                _classCards.Add(card);
            }
        }


        public void UpdateUI(IReadOnlyList<PlayerClassData> availableClasses, int totalLevel)
        {
            _levelText.text = $"Level: {totalLevel + 1}";

            for (int i = 0; i < availableClasses.Count; i++)
            {
                if (i >= _classCards.Count) break;

                if (availableClasses[i].Level > availableClasses[i].Class.MaxLevel)
                {
                    _classCards[i].Hide();
                }
                else
                {
                    _classCards[i].SetData(availableClasses[i], totalLevel);
                }
            }
        }


        void AddClassLevelButtonPressed(ClassCard classCard)
        {
            OnClassLevelAdded?.Invoke(_classCards.IndexOf(classCard));
        }
    }
}