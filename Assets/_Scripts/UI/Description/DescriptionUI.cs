using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.Description
{
    public class DescriptionUI : TemplateUI
    {
        [SerializeField] private Transform _beacon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;


        public void SetData(Sprite sprite, string name, string description)
        {
            _icon.sprite = sprite;
            _name.text = name;
            _description.text = description;
        }

        public void SetPosition(Vector3 position)
        {
            _beacon.transform.position = position;
        }
    }
}