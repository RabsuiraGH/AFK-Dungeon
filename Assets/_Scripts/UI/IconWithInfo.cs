using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.UI
{
    public class IconWithInfo : TemplateUI, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] public Image _iconImage;
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        public static event Action<IconWithInfo> OnHoverEnter;
        public static event Action<IconWithInfo> OnHoverExit;


        private void Start()
        {
            if(Sprite == null && _iconImage.sprite != null)
            {
                Sprite = _iconImage.sprite;
            }
        }


        public void SetData(Sprite sprite, string name, string description)
        {
            _iconImage.sprite = sprite;
            _iconImage.enabled = true;
            Sprite = sprite;
            Name = name;
            Description = description;
        }


        public void OnPointerEnter(PointerEventData eventData) => OnHoverEnter?.Invoke(this);

        public void OnPointerExit(PointerEventData eventData) => OnHoverExit?.Invoke(this);
    }
}