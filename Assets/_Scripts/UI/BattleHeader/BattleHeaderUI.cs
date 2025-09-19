using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.BattleHeader
{
    public class BattleHeaderUI : TemplateUI
    {
        [SerializeField] private TextMeshProUGUI _turnText;
        [SerializeField] private List<Button> _speedButtons = new();

        public event Action<int> OnSpeedButtonClicked;


        public void Setup(List<float> speeds)
        {
            for (int i = 0; i < _speedButtons.Count; i++)
            {
                if (i < speeds.Count)
                {
                    int index = i;
                    _speedButtons[i].onClick.AddListener(() => OnSpeedButtonClicked?.Invoke(index));
                    _speedButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = $"x{speeds[i]}";
                    _speedButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    _speedButtons[i].gameObject.SetActive(false);
                }
            }
        }


        public void SetTurn(int turn)
        {
            _turnText.text = $"TURN: {turn}";
        }


        private void OnDestroy()
        {
            foreach (Button button in _speedButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}