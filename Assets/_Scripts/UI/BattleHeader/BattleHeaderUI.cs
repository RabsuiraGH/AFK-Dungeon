using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.BattleHeader
{
    public class BattleHeaderUI : TemplateUI
    {
        [SerializeField] private Button _openMenuButton;
        [SerializeField] private TextMeshProUGUI _turnText;
        [SerializeField] private TextMeshProUGUI _battleCounterText;
        [SerializeField] private Toggle _pauseButton;
        [SerializeField] private List<Toggle> _speedButtons = new();

        public event Action<int> OnSpeedButtonClicked;
        public event Action OnPauseButtonClicked;

        public event Action OnMenuButtonClicked;


        public void Setup(List<float> speeds)
        {
            _openMenuButton.onClick.AddListener(() => OnMenuButtonClicked?.Invoke());

            _pauseButton.onValueChanged.AddListener((val) =>
            {
                if (val) OnPauseButtonClicked?.Invoke();
            });

            for (int i = 0; i < _speedButtons.Count; i++)
            {
                if (i < speeds.Count)
                {
                    int index = i;
                    _speedButtons[i].onValueChanged.AddListener((val) =>
                    {
                        if (val) OnSpeedButtonClicked?.Invoke(index);
                    });

                    _speedButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    _speedButtons[i].gameObject.SetActive(false);
                }
            }
            _speedButtons[0].SetIsOnWithoutNotify(true);
        }


        public void SetTurn(int turn)
        {
            _turnText.text = $"TURN: {turn}";
        }

        public void SetBattleCounter(int battle)
        {
            _battleCounterText.text = $"BATTLE: {battle + 1}";
        }


        private void OnDestroy()
        {
            _openMenuButton.onClick.RemoveAllListeners();
            _pauseButton.onValueChanged.RemoveAllListeners();

            foreach (Toggle button in _speedButtons)
            {
                button.onValueChanged.RemoveAllListeners();
            }
        }
    }
}