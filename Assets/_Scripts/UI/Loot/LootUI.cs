using System;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.Loot
{
    public class LootUI : TemplateUI
    {
        [SerializeField] private IconWithInfo _weaponIcon;
        [SerializeField] private Button _claimButton;
        [SerializeField] private Button _discardButton;


        public event Action OnClaimButtonClicked;
        public event Action OnDiscardButtonClicked;
        private void Start()
        {
            _claimButton.onClick.AddListener(() => OnClaimButtonClicked?.Invoke());
            _discardButton.onClick.AddListener(() => OnDiscardButtonClicked?.Invoke());
        }


        public void SetWeapon(Sprite sprite, string name, string description)
        {
            _weaponIcon.SetData(sprite, name, description);
        }


        private void OnDestroy()
        {
            _claimButton.onClick.RemoveAllListeners();
            _discardButton.onClick.RemoveAllListeners();
        }
    }
}