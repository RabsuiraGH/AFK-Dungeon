using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace LA.UI
{
    public class BattleResultPopupUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _resultText;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _fadeDilay = 0.5f;
        [SerializeField] private float _fadeDuration = 1f;


        public IEnumerator ShowPopup(string text)
        {
            _resultText.text = text;
            _canvasGroup.alpha = 1;
            Show();
            yield return StartCoroutine(Fade(1, 0));
        }


        private IEnumerator Fade(float start, float end)
        {
            yield return new WaitForSeconds(_fadeDilay);
            float time = 0;


            while (time < _fadeDuration)
            {
                time += Time.deltaTime;

                _canvasGroup.alpha = Mathf.Lerp(start, end, time / _fadeDuration);
                yield return null;
            }


            _canvasGroup.alpha = end;
            Hide();
        }
    }
}