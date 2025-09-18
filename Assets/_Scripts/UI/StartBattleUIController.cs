using System;
using UnityEngine;

namespace LA.UI
{
    public class StartBattleUIController : MonoBehaviour
    {
        [SerializeField] private StartBattleUI _startBattleUI;

        public event Action OnStartBattleRequested;


        private void Start()
        {
            _startBattleUI.OnStartBattleRequested += RequestBattleStart;
        }


        public void Show() => _startBattleUI.Show();


        private void RequestBattleStart()
        {
            OnStartBattleRequested?.Invoke();
            _startBattleUI.Hide();
        }


        private void OnDestroy()
        {
            _startBattleUI.OnStartBattleRequested -= RequestBattleStart;
        }
    }
}