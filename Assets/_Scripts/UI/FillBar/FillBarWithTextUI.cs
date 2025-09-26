using TMPro;
using UnityEngine;

namespace LA.UI.FillBar
{
    public class FillBarWithTextUI : FillBarUI
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private string _separator = "/";
        [SerializeField] private bool _allowsNegativeValues = false;


        public override void Fill(float currentValue, float maxValue)
        {
            base.Fill(currentValue, maxValue);

            if(!_allowsNegativeValues && currentValue < 0) currentValue = 0;

            _valueText.text = $"{currentValue}{_separator}{maxValue}";
        }
    }
}