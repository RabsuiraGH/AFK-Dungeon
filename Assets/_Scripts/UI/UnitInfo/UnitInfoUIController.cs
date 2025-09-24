using System;
using LA.Gameplay;
using LA.Gameplay.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.UnitInfo
{
    public class UnitInfoUIController : MonoBehaviour
    {
        [SerializeField] private Image _playerImage;
        [SerializeField] private Image _enemyImage;

        [SerializeField] private UnitInfoUI _playerInfoUI;
        [SerializeField] private UnitInfoUI _enemyInfoUI;

        private BattleService _battleService;


        [VContainer.Inject]
        public void Construct(BattleService battleService)
        {
            _battleService = battleService;

            _battleService.OnPlayerUpdates += UpdatePlayerHealth;
            _battleService.OnEnemyUpdates += UpdateEnemyHealth;

            _playerInfoUI.Hide();
            _enemyInfoUI.Hide();
            _playerImage.gameObject.SetActive(false);
            _enemyImage.gameObject.SetActive(false);
        }


        public void SetPlayerInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, _playerInfoUI);
            _playerImage.sprite = unit.Sprite;
            _playerImage.gameObject.SetActive(true);

        }


        public void SetEnemyInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, _enemyInfoUI);
            _enemyImage.sprite = unit.Sprite;
            _enemyImage.gameObject.SetActive(true);
        }


        public void HidePlayerInfo() => _playerInfoUI.Hide();

        public void HideEnemyInfo() => _enemyInfoUI.Hide();


        private void UpdatePlayerHealth(BattleUnit unit)
        {
            UpdateHealth(unit, _playerInfoUI);
        }


        private void UpdateEnemyHealth(BattleUnit unit)
        {
            UpdateHealth(unit, _enemyInfoUI);
        }


        public void SetUnitInfo(BattleUnit unit, UnitInfoUI unitInfoUI)
        {
            unitInfoUI.SetUnitName(unit.Name);
            unitInfoUI.SetHealth(unit.CurrentHealth, unit.MaxHealth);
            unitInfoUI.SetStats(unit.Stats.GetStats());
            unitInfoUI.SetWeaponData(unit.CurrentWeapon.WeaponSource);
            unitInfoUI.SetAbilities(unit.Abilities);

            unitInfoUI.Show();
        }


        public void UpdateHealth(BattleUnit unit, UnitInfoUI unitInfoUI)
        {
            unitInfoUI.SetHealth(unit.CurrentHealth, unit.MaxHealth);
        }


        private void OnDestroy()
        {
            _battleService.OnPlayerUpdates -= UpdatePlayerHealth;
            _battleService.OnEnemyUpdates -= UpdateEnemyHealth;
        }
    }
}