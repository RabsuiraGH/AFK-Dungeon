using System;
using System.Collections;
using DG.Tweening;
using LA.Gameplay;
using LA.Gameplay.GameLoop;
using LA.Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.UnitInfo
{
    public class UnitInfoUIController : MonoBehaviour
    {
        [SerializeField] private UnitInfoUI _playerInfoUI;
        [SerializeField] private UnitInfoUI _enemyInfoUI;

        private GameService _battleService;


        [VContainer.Inject]
        public void Construct(GameService battleService)
        {
            _battleService = battleService;

            _battleService.OnUnitUpdates += UpdateUnit;

            _battleService.OnUnitAttack += OnUnitAttack;

            _playerInfoUI.Hide();
            _enemyInfoUI.Hide();
        }


        private void OnUnitAttack(BattleUnit attackingUnit, bool isHit)
        {
            GetUnitInfoUI(attackingUnit).AnimateAttack(_battleService.GetTurnDuration());
            if (!isHit)
            {
                GetOpponentInfoUI(attackingUnit).AnimateDodge(_battleService.GetTurnDuration());
            }
        }


        public void SetUnitInfo(BattleUnit unit)
        {
            SetUnitInfo(unit, GetUnitInfoUI(unit));
        }


        public void HidePlayerInfo() => _playerInfoUI.Hide();

        public void HideEnemyInfo() => _enemyInfoUI.Hide();


        private void UpdateUnit(BattleUnit unit)
        {
            UpdateHealth(unit, GetUnitInfoUI(unit));
        }


        private void SetUnitInfo(BattleUnit unit, UnitInfoUI unitInfoUI)
        {
            unitInfoUI.SetUnitImage(unit.Sprite);
            unitInfoUI.SetUnitName(unit.Name);
            unitInfoUI.SetHealth(unit.CurrentHealth, unit.MaxHealth);
            unitInfoUI.SetStats(unit.Stats.GetStats());
            unitInfoUI.SetWeaponData(unit.CurrentWeapon.WeaponSource);
            unitInfoUI.SetAbilities(unit.Abilities);

            unitInfoUI.Show();
        }


        private void UpdateHealth(BattleUnit unit, UnitInfoUI unitInfoUI)
        {
            unitInfoUI.SetHealth(unit.CurrentHealth, unit.MaxHealth);
        }


        private UnitInfoUI GetUnitInfoUI(BattleUnit unit) => unit is Player ? _playerInfoUI : _enemyInfoUI;

        private UnitInfoUI GetOpponentInfoUI(BattleUnit unit) => unit is not Player ? _playerInfoUI : _enemyInfoUI;


        private void OnDestroy()
        {
            _battleService.OnUnitUpdates -= UpdateUnit;
            _battleService.OnUnitAttack -= OnUnitAttack;
        }
    }
}