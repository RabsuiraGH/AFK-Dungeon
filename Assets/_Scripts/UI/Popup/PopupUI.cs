using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LA.UI.UnitInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.Popup
{
    public class PopupUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        private Sequence _seq;


        public static PopupUI CreatePopup(PopupUI prefab, Vector3 position, Transform parent = null)
        {
            PopupUI popupUI = Instantiate(prefab, position, Quaternion.identity, parent);
            popupUI.OpenSequence();
            return popupUI;
        }


        public PopupUI SetText(string text)
        {
            _text.text = text;
            _text.gameObject.SetActive(true);
            return this;
        }


        public PopupUI SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.gameObject.SetActive(true);

            return this;
        }


        public PopupUI MovePopup(Vector3 direction, float duration)
        {
            _seq.Append(transform.DOMove(transform.position + direction, duration).SetEase(Ease.InOutSine));
            return this;
        }


        public PopupUI FadePopup(float endValue, float duration)
        {
            _seq.Append(_canvasGroup.DOFade(0, duration).SetEase(Ease.InOutQuart));
            return this;
        }


        public PopupUI DestroyOnComplete()
        {
            _seq.OnComplete(() => Destroy(gameObject));
            return this;
        }


        private void OpenSequence()
        {
            _seq = DOTween.Sequence().SetLink(this.gameObject);
        }
    }
}