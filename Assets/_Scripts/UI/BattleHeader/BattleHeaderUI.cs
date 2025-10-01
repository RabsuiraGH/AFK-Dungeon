using System;
using System.Collections.Generic;
using System.Linq;
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

            _pauseButton.onValueChanged.AddListener(SetPause);

            for (int i = 0; i < Math.Min(_speedButtons.Count, speeds.Count); i++)
            {
                int index = i;
                _speedButtons[i].onValueChanged.AddListener((val) =>
                {
                    if (val) OnSpeedButtonClicked?.Invoke(index);
                });
            }

            _speedButtons[0].SetIsOnWithoutNotify(true);
        }


        private void SetPause(bool pause)
        {
            if (pause) return;

            OnPauseButtonClicked?.Invoke();
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