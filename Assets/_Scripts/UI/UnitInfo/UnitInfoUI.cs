using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.Stat;
using LA.Gameplay.WeaponSystem;
using LA.UI.FillBar;
using TMPro;
using UnityEngine;

namespace LA.UI.UnitInfo
{
    public class UnitInfoUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _unitName;
        [SerializeField] private FillBarUI _healthBar;
        [SerializeField] private StatCard _agilityCard;
        [SerializeField] private StatCard _strengthCard;
        [SerializeField] private StatCard _enduranceCard;
        [SerializeField] private IconWithInfo _weaponIcon;
        [SerializeField] private List<IconWithInfo> _abilityIcons;

        public void SetUnitName(string unitName) => _unitName.text = unitName;

        public void SetHealth(float currentValue, float maxValue) => _healthBar.Fill(currentValue, maxValue);


        public void SetStats(Dictionary<StatType, int> stats)
        {
            _agilityCard.UpdateData(stats[StatType.Agility].ToString());
            _strengthCard.UpdateData(stats[StatType.Strength].ToString());
            _enduranceCard.UpdateData(stats[StatType.Endurance].ToString());
        }


        public void SetAbilities(List<AbilitySO> newAbilities)
        {
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


        public void SetWeaponData(WeaponSO weaponSource)
        {
            _weaponIcon.SetData(weaponSource.Sprite, weaponSource.Name, weaponSource.Description);
        }
    }
}