using TMPro;
using UnityEngine;

namespace LA.UI
{
    public class DescriptionPopup : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _description;

        public void SetPosition(Vector3 position) => transform.position = position;
    }
}