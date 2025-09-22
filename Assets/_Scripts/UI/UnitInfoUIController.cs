using System;
using UnityEngine;

namespace LA.UI
{
    public class UnitInfoUIController : MonoBehaviour
    {
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
        }


        public void SetPlayerInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, _playerInfoUI);
        }


        public void SetEnemyInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, _enemyInfoUI);
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