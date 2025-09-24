using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.Player.ClassSystem;
using LA.Gameplay.Stat;
using LA.Gameplay.WeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.UI.PlayerClassSelector
{
    public class ClassCard : TemplateUI, IPointerClickHandler
    {
        [SerializeField] private Image _classIcon;
        [SerializeField] private TextMeshProUGUI _className;

        [SerializeField] private Transform _weaponPanel;
        [SerializeField] private IconWithInfo _weaponIcon;

        [SerializeField] private TextMeshProUGUI _healthUpgradeText;

        [SerializeField] private Transform _statCardsPanel;
        [SerializeField] private List<StatCard> _statCards;

        [SerializeField] private Transform _abilitiesPanel;
        [SerializeField] private List<IconWithInfo> _abilityIcons = new();
        [SerializeField] private Transform _abilityIconContainer;

        public event Action<ClassCard> OnClick;


        public void SetData(PlayerClassData classData, int totalLevel)
        {
            _classIcon.sprite = classData.Class.ClassIcon;
            _className.text = classData.Class.ClassName;
            _healthUpgradeText.text = $"HP: +{classData.Class.HealthPerLevel.ToString()}";

            DisplayUpgrade(classData.Class.LevelUpgrades[classData.Level - 1]);

            if (totalLevel == 0)
            {
                DisplayWeapon(classData.Class.StartWeapon);
            }
            else
            {
                _weaponPanel.gameObject.SetActive(false);
            }
        }


        private void DisplayUpgrade(LevelUpgrade classLevelUpgrade)
        {
            AddStatUpgrades(classLevelUpgrade.StatUpgrades);
            AddAbilities(classLevelUpgrade.NewAbilities);
        }


        private void DisplayWeapon(WeaponSO classStartWeapon)
        {
            _weaponIcon.SetData(classStartWeapon.Sprite, classStartWeapon.Name, classStartWeapon.Description);
            _weaponPanel.gameObject.SetActive(true);
        }


        private void AddStatUpgrades(Stats statUpgrades)
        {
            bool anyVisible = false;

            foreach (StatCard card in _statCards)
            {
                int statValue = statUpgrades.GetStat(card.StatType);

                if (statValue != 0)
                {
                    card.UpdateData(statValue);
                    card.Show();

                    anyVisible = true;
                }
                else
                {
                    card.Hide();
                }
            }

            _statCardsPanel.gameObject.SetActive(anyVisible);
        }


        private void AddAbilities(List<AbilitySO> newAbilities)
        {
            bool hasAbilities = newAbilities.Any();

            _abilitiesPanel.gameObject.SetActive(hasAbilities);

            if (!hasAbilities) return;

            for (int i = 0; i < _abilityIcons.Count; i++)
            {
                if (i < newAbilities.Count)
                {
                    _abilityIcons[i].SetData(newAbilities[i].Icon, newAbilities[i].Name, newAbilities[i].Description);
                    _abilityIcons[i].Show();
                }
                else
                {
                    _abilityIcons[i].Hide();
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