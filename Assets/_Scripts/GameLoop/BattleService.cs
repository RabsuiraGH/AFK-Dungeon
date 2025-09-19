using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.AbilitySystem.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LA
{
    [Serializable]
    public class BattleService
    {
        [SerializeField] private Enemy _enemy;

        [SerializeField] private Player _player;

        private BattleUnit _attackingUnit;
        private BattleUnit _defendingUnit;

        [SerializeField] private int _currentTurn = 0;
        private Dictionary<BattleUnit, int> _turnCounters = new();

        public event Action<BattleUnit> OnPlayerUpdates;
        public event Action<BattleUnit> OnEnemyUpdates;

        public event Action<BattleUnit> OnEnemySet;

        public event Action OnPlayerWin;
        public event Action<Enemy> OnPlayerLose;

        private IRandomService _randomService;


        [VContainer.Inject]
        public void Construct(IRandomService randomService)
        {
            _randomService = randomService;
        }


        public void NextTurn()
        {
            _currentTurn += 1;

            AddTurn(_attackingUnit);

            BattleContext context = new()
            {
                TurnNumber = _currentTurn,
                Attacker = _attackingUnit,
                Defender = _defendingUnit,
                TurnCounters = new Dictionary<BattleUnit, int>(_turnCounters)
            };
            context.Init();

            int hitChance =
                _randomService.Range(1, context.Attacker.Stats.Agility + context.Defender.Stats.Agility + 1);


            bool isHit = _defendingUnit.IsHit(hitChance);

            Debug.Log(($"{context.Attacker.Name}  || {context.Defender.Name}"));

            OnBeforeHitAbilities(context.Attacker);
            OnBeforeHitAbilities(context.Defender);

            int totalDamage = 0;
            if (isHit)
            {
                totalDamage = context.GetTotalDamage(context.Attacker);

                if (totalDamage > 0)
                {
                    context.Defender.TakeDamage(totalDamage);
                }
            }


            Debug.Log(($"{_currentTurn} - {context.Attacker.Name}: Hit chance: {hitChance} | Is hit: {isHit} | Damage: {totalDamage}"));

            totalDamage = context.GetTotalOtherDamage(context.Defender);
            context.Attacker.TakeDamage(totalDamage);


            OnPlayerUpdates?.Invoke(_player);
            OnEnemyUpdates?.Invoke(_enemy);

            void OnBeforeHitAbilities(BattleUnit owner)
            {
                context.AbilityOwner = owner;
                foreach (IOnBeforeHitAbility ability in context.AbilityOwner.Abilities.OfType<IOnBeforeHitAbility>())
                {
                    ability.OnBeforeHit(context);
                }
            }
        }


        public void SetEnemy(Enemy enemyBase)
        {
            _enemy = enemyBase;
            OnEnemySet?.Invoke(_enemy);
        }

        public Enemy GetEnemy()
        {
            return _enemy;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
            _player.RestoreHealth();
        }


        public void ResetBattle()
        {
            _currentTurn = 0;
            _turnCounters = new Dictionary<BattleUnit, int>();
        }


        public bool CheckBattleEnd()
        {
            return _player.IsDead() || _enemy.IsDead();
        }


        public void OnBattleEnd()
        {
            if (!_player.IsDead())
            {
                OnPlayerWin?.Invoke();
            }
            else
            {
                OnPlayerLose?.Invoke(_enemy);
            }
        }


        private void AddTurn(BattleUnit unit)
        {
            _turnCounters.TryAdd(unit, 0);
            _turnCounters[_attackingUnit]++;
        }


        public void DecideFirstTurn()
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


        public void SwapUnits() => (_attackingUnit, _defendingUnit) = (_defendingUnit, _attackingUnit);
    }
}