using TMPro;
using UnityEngine;

namespace LA.UI
{
    public class UnitInfoUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _unitName;
        [SerializeField] private FillBarUI _healthBar;
        [SerializeField] private TextMeshProUGUI _agilityText;
        [SerializeField] private TextMeshProUGUI _strengthText;
        [SerializeField] private TextMeshProUGUI _enduranceText;
        [SerializeField] private IconWithInfo _weaponIcon;


        public void SetUnitName(string unitName) => _unitName.text = unitName;

        public void SetHealth(float currentValue, float maxValue) => _healthBar.Fill(currentValue, maxValue);

        public void SetAgility(int value) => _agilityText.text = value.ToString();

        public void SetStrength(int value) => _strengthText.text = value.ToString();

        public void SetEndurance(int value) => _enduranceText.text = value.ToString();
        public void SetWeaponIcon(Sprite sprite) => _weaponIcon.SetIcon(sprite);
    }
}