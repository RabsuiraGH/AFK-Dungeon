using System;
using LA.WeaponSystem;
using UnityEngine;

namespace LA.UI.Loot
{
    public class LootUIController : MonoBehaviour
    {
        [SerializeField] private LootUI _lootUI;

        private WeaponSO _currentWeapon;

        private Player _player;

        public event Action OnChoiceMade;


        [VContainer.Inject]
        public void Construct(Player player)
        {

            _player = player;
        }


        public void OnPlayerWin(WeaponSO loot)
        {
            SetWeapon(loot);
            _lootUI.Show();
        }


        private void Start()
        {
            _lootUI.OnClaimButtonClicked += ClaimLoot;
            _lootUI.OnDiscardButtonClicked += DiscardLoot;
        }


        private void DiscardLoot()
        {
            OnChoiceMade?.Invoke();
            _lootUI.Hide();
        }


        private void ClaimLoot()
        {
            _player.CurrentWeapon = new Weapon(_currentWeapon);
            OnChoiceMade?.Invoke();
            _lootUI.Hide();
        }


        private void SetWeapon(WeaponSO weapon)
        {
            _currentWeapon = weapon;
            _lootUI.SetWeapon(weapon.Sprite, weapon.Name, weapon.Description);
        }
    }
}