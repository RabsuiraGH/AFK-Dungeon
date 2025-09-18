using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.UI
{
    public class IconWithInfo : TemplateUI, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] public Image _iconImage;
        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public static event Action<IconWithInfo> OnHoverEnter;
        public static event Action<IconWithInfo> OnHoverExit;


        public void SetData(Sprite sprite, string name, string description)
        {
            _iconImage.sprite = sprite;
            Sprite = sprite;
            Name = name;
            Description = description;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter?.Invoke(this);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit?.Invoke(this);
        }
    }
}