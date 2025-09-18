using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI
{
    public class StatCard : TemplateUI
    {
        [field: SerializeField] public StatType StatType;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _text;


        public void SetType(StatType statType)
        {
            StatType = statType;
        }


        public void SetData(Sprite icon, string text)
        {
            _icon.sprite = icon;
            _text.text = text;
        }


        public void SetData(Sprite icon, int value)
        {
            _icon.sprite = icon;
            _text.text = GetBasicText(value);
        }


        public void UpdateData(string text)
        {
            _text.text = text;
        }


        public void UpdateData(int value)
        {
            _text.text = GetBasicText(value);
        }


        private string GetBasicText(int value)
        {
            string sign = value > 0 ? "+" : "";
            return $"{sign}{value}";
        }
    }
}