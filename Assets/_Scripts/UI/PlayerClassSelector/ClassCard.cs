using System;
using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.UI
{
    public class ClassCard : TemplateUI, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _name;

        [SerializeField] private List<IconWithInfo> _newAbilities = new();
        [SerializeField] private Transform _abilitiesContainer;
        [SerializeField] private List<StatCard> _statCards;

        [SerializeField] private IconWithInfo _iconWithInfoPrefab;

        public event Action<ClassCard> OnClick;


        public void SetData(PlayerClassData classData)
        {
            _image.sprite = classData.Class.ClassIcon;
            _name.text = classData.Class.ClassName;

            DisplayUpgrade(classData.Class.LevelUpgrades[classData.Level - 1]);
        }


        private void DisplayUpgrade(LevelUpgrade classLevelUpgrade)
        {
            // TODO: Add dynamic container creation
            AddStatUpgrades(classLevelUpgrade.StatUpgrades);
            AddAbilities(classLevelUpgrade.NewAbilities);
        }


        private void AddStatUpgrades(Stats statUpgrades)
        {
            foreach (StatCard card in _statCards)
            {
                int statValue = statUpgrades.GetStat(card.StatType);

                if (statValue != 0)
                {
                    card.UpdateData(statValue);
                    card.Show();
                }
                else
                {
                    card.Hide();
                }
            }
        }


        private void AddAbilities(List<AbilitySO> newAbilities)
        {
            for (int i = 0; i < _newAbilities.Count; i++)
            {
                _newAbilities[i].Hide();
            }


            for (int i = 0; i < newAbilities.Count; i++)
            {
                Debug.Log(($"{newAbilities[i].Name} {_newAbilities.Count < i}"));
                if (i < _newAbilities.Count)
                {
                    _newAbilities[i].SetData(newAbilities[i].Icon, newAbilities[i].Name, newAbilities[i].Description);
                    _newAbilities[i].Show();
                }
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnClick?.Invoke(this);
            }
        }


        private void OnDestroy()
        {
            OnClick = null;
        }
    }
}