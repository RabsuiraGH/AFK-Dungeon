using TMPro;
using UnityEngine;

namespace LA.UI.FillBar
{
    public class FillBarWithTextUI : FillBarUI
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private string _separator = "/";


        public override void Fill(float currentValue, float maxValue)
        {
            base.Fill(currentValue, maxValue);
            _valueText.text = $"{currentValue}{_separator}{maxValue}";
        }
    }
}