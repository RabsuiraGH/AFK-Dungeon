using System;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.StartBattle
{
    public class StartBattleUI : TemplateUI
    {
        [SerializeField] private Button _startBattleButton;

        public event Action OnStartBattleRequested;

        private void Start() => _startBattleButton.onClick.AddListener(OnStartBattleButtonClicked);

        private void OnStartBattleButtonClicked() => OnStartBattleRequested?.Invoke();

        private void OnDestroy() => _startBattleButton.onClick.RemoveListener(OnStartBattleButtonClicked);
    }
}