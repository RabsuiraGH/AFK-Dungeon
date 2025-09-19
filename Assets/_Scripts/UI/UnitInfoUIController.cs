using System;
using UnityEngine;

namespace LA.UI
{
    public class UnitInfoUIController : MonoBehaviour
    {
        [SerializeField] private UnitInfoUI _playerInfoUI;
        [SerializeField] private UnitInfoUI _enemyInfoUI;

        private MainGameLoop _mainGameLoop;


        [VContainer.Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;

            _mainGameLoop.OnPlayerUpdates += UpdatePlayerHealth;
            _mainGameLoop.OnEnemyUpdates += UpdateEnemyHealth;
        }


        public void SetPlayerInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, _playerInfoUI);
        }


        public void SetEnemyInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, _enemyInfoUI);
        }


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
            unitInfoUI.SetWeaponData(unit.CurrentWeapon.WeaponSource.Sprite, unit.CurrentWeapon.WeaponSource.Name,
                                     unit.CurrentWeapon.WeaponSource.Description);

            unitInfoUI.Show();
        }


        public void UpdateHealth(BattleUnit unit, UnitInfoUI unitInfoUI)
        {
            unitInfoUI.SetHealth(unit.CurrentHealth, unit.MaxHealth);
        }


        private void OnDestroy()
        {
            _mainGameLoop.OnPlayerUpdates -= UpdatePlayerHealth;
            _mainGameLoop.OnEnemyUpdates -= UpdateEnemyHealth;
        }
    }
}