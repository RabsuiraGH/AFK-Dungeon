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


        public IEnumerator ShowPopup(string text, float fadeInDuration, float fadeOutDuration, float stayDuration)
        {
            _resultText.text = text;
            Show();
            yield return StartCoroutine(Fade(fadeInDuration, fadeOutDuration, stayDuration));
        }


        private IEnumerator Fade(float fadeInDuration, float fadeOutDuration, float delay)
        {
            float time = 0;
            float zeroAlpha = 0;
            float oneAlpha = 1;

            _canvasGroup.alpha = 0;
            while (time < fadeInDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(zeroAlpha, oneAlpha, time / fadeInDuration);
                yield return null;
            }

            _canvasGroup.alpha = oneAlpha;

            yield return new WaitForSeconds(delay);

            time = 0;
            while (time < fadeOutDuration)
            {
                time += Time.deltaTime;

                _canvasGroup.alpha = Mathf.Lerp(oneAlpha, zeroAlpha, time / fadeOutDuration);
                yield return null;
            }

            _canvasGroup.alpha = zeroAlpha;
            Hide();
        }
    }
}