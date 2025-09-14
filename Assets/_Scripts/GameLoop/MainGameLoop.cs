using System;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LA
{
    [Serializable]
    public class MainGameLoop
    {
        [SerializeField] private Enemy _enemy;

        [SerializeField] private Player _player;

        [SerializeField] private BattleUnit _attackingUnit;
        [SerializeField] private BattleUnit _defendingUnit;

        [SerializeField] private int _currentTurn = 0;

        public event Action<BattleUnit> OnPlayerAppears;
        public event Action<BattleUnit> OnEnemyAppears;

        public event Action<BattleUnit> OnPlayerUpdates;
        public event Action<BattleUnit> OnEnemyUpdates;


        [VContainer.Inject]
        public void Construct(Player player)
        {
            _player = player;
        }


        public void CreateEnemy(EnemySO enemyBase)
        {
            _enemy = new Enemy(enemyBase);
            _enemy.Init();
        }


        public void Reset()
        {
            _currentTurn = 0;
        }


        public bool CheckWin()
        {
            return _player.IsDead() || _enemy.IsDead();
        }


        public string GetWinText()
        {
            return _player.IsDead() ? "You Lose" : "You Win";
        }


        public void SimulateTurn()
        {
            _currentTurn += 1;

            if (_currentTurn == 1)
            {
                OnEnemyAppears?.Invoke(_enemy);
                OnPlayerAppears?.Invoke(_player);
                DecideFirstTurn();
            }

            BattleContext context = new()
            {
                TurnNumber = _currentTurn,
                Attacker = _attackingUnit,
                Defender = _defendingUnit,
                WeaponDamage = _attackingUnit.CurrentWeapon.ToDamageContext()
            };

            int hitChance = Random.Range(1, context.Attacker.Stats.Agility + context.Defender.Stats.Agility + 1);


            bool isHit = _defendingUnit.IsHit(hitChance);


            foreach (IOnBeforeHitAbility beforeHitAbility in context.Attacker.Abilities.OfType<IOffensiveAbility>()
                                                                    .OfType<IOnBeforeHitAbility>())
            {
                beforeHitAbility.OnBeforeHit(context);
            }

            foreach (IOnBeforeHitAbility beforeHitAbility in context.Defender.Abilities.OfType<IDefensiveAbility>()
                                                                    .OfType<IOnBeforeHitAbility>())
            {
                beforeHitAbility.OnBeforeHit(context);
            }

            int totalDamage = 0;
            if (isHit)
            {
                totalDamage = context.GetTotalDamage();

                context.Defender.TakeDamage(totalDamage);
            }

            Debug.Log(($"{_currentTurn}-{_attackingUnit.GetType().Name}: Hit chance: {hitChance} | Is hit: {isHit} | Damage: {totalDamage}"));

            OnPlayerUpdates?.Invoke(_player);
            OnEnemyUpdates?.Invoke(_enemy);
            SwapUnits();
        }


        private void DecideFirstTurn()
        {
            if (_player.Stats.Agility >= _enemy.Stats.Agility)
            {
                _attackingUnit = _player;
                _defendingUnit = _enemy;
            }
            else
            {
                _attackingUnit = _enemy;
                _defendingUnit = _player;
            }
        }


        private void SwapUnits()
        {
            (_attackingUnit, _defendingUnit) = (_defendingUnit, _attackingUnit);
        }
    }
}