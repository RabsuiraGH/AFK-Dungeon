using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LA.UI
{
    public class UnitInfoUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _unitName;
        [SerializeField] private FillBarUI _healthBar;
        [SerializeField] private StatCard _agilityCard;
        [SerializeField] private StatCard _strengthCard;
        [SerializeField] private StatCard _enduranceCard;
        [SerializeField] private IconWithInfo _weaponIcon;

        public void SetUnitName(string unitName) => _unitName.text = unitName;

        public void SetHealth(float currentValue, float maxValue) => _healthBar.Fill(currentValue, maxValue);


        public void SetStats(Dictionary<StatType, int> stats)
        {
            _agilityCard.UpdateData(stats[StatType.Agility].ToString());
            _strengthCard.UpdateData(stats[StatType.Strength].ToString());
            _enduranceCard.UpdateData(stats[StatType.Endurance].ToString());
        }


        public void SetWeaponData(Sprite sprite, string name, string description)
        {
            _weaponIcon.SetData(sprite, name, description);
        }
    }
}