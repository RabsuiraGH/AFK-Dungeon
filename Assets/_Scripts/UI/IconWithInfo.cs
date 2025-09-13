using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.UI
{
    public class IconWithInfo : TemplateUI, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _iconImage;
        public event Action<Transform> OnHoverEnter;
        public event Action<Transform> OnHoverExit;


        public void SetIcon(Sprite sprite)
        {
            _iconImage.sprite = sprite;
        }


        public void HideIcon()
        {
            _iconImage.enabled = false;
        }


        public void ShowIcon()
        {
            _iconImage.enabled = true;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter?.Invoke(this.transform);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit?.Invoke(this.transform);
        }
    }
}