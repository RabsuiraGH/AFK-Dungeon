using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.UI.MainMenu
{
    public class MainMenuUI : TemplateUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        private Sequence _fadeTween;
        public event Action OnStartButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnQuitButtonClicked;


        private void Awake()
        {
            _canvasGroup.alpha = 0;

            _fadeTween = DOTween.Sequence().SetDelay(2f).Append(_canvasGroup.DOFade(1, 5f)).SetLink(this.gameObject);
        }


        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }


        public void InstantFade()
        {
            _fadeTween.Kill(true);
        }

            private void Start()
        {
            _startButton.onClick.AddListener(() => OnStartButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
            _quitButton.onClick.AddListener(() => OnQuitButtonClicked?.Invoke());


        }
    }
}