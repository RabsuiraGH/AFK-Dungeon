using System;
using System.Collections.Generic;
using LA.Gameplay.Player.ClassSystem;
using LA.Gameplay.Stat;
using TMPro;
using UnityEngine;

namespace LA.UI.PlayerClassSelector
{
    public class PlayerClassSelectorUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Transform _contentPanel;
        [SerializeField] private List<StatCard> _statCards;
        [SerializeField] private StatCard _healthStatCard;
        [SerializeField] private List<ClassCard> _classCards;

        [SerializeField] private ClassCard _classCardPrefab;
        public event Action<int> OnClassLevelAdded;

        bool _initialized = false;


        public void PrepareUI(IReadOnlyList<PlayerClassData> availableClasses, int totalLevel)
        {
            if (!_initialized)
            {
                Init(availableClasses, totalLevel);
            }
            else
            {
                UpdateUI(availableClasses, totalLevel);
            }
        }


        private void Init(IReadOnlyList<PlayerClassData> availableClasses, int totalLevel)
        {
            _initialized = true;
            _levelText.text = $"Level: {totalLevel + 1}";

            foreach (PlayerClassData classData in availableClasses)
            {
                ClassCard card = Instantiate(_classCardPrefab, _contentPanel);

                card.SetData(classData, totalLevel);

                card.OnClick += AddClassLevelButtonPressed;
                _classCards.Add(card);
            }
        }


        private void UpdateUI(IReadOnlyList<PlayerClassData> availableClasses, int totalLevel)
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


        public void UpdateStats(Stats statUpgrades)
        {
            foreach (StatCard card in _statCards)
            {
                int statValue = statUpgrades.GetStat(card.StatType);

                card.UpdateData(statValue.ToString());
            }
        }


        public void UpdateHealth(int maxHealth)
        {
            _healthStatCard.UpdateData(maxHealth.ToString());
        }


        private void AddClassLevelButtonPressed(ClassCard classCard) =>
            OnClassLevelAdded?.Invoke(_classCards.IndexOf(classCard));
    }
}