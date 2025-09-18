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
    public class MainGameLoop
    {
        [field: SerializeField] public int BattleCounter { get; private set; } = 0;

        [SerializeField] private Enemy _enemy;

        [SerializeField] private Player _player;

        [SerializeField] private BattleUnit _attackingUnit;
        [SerializeField] private BattleUnit _defendingUnit;

        [SerializeField] private int _currentTurn = 0;
        private Dictionary<BattleUnit, int> _turnCounters = new();

        public event Action<BattleUnit> OnPlayerAppears;
        public event Action<BattleUnit> OnEnemyAppears;

        public event Action<BattleUnit> OnPlayerUpdates;
        public event Action<BattleUnit> OnEnemyUpdates;

        public event Action OnPlayerWin;
        public event Action<Enemy> OnPlayerLose;

        private IRandomService _randomService;


        [VContainer.Inject]
        public void Construct(Player player, IRandomService randomService)
        {
            _randomService = randomService;
            _player = player;
        }


        public void CreateEnemy(EnemySO enemyBase)
        {
            _enemy = new Enemy(enemyBase);
            _enemy.Init();
        }


        public void ResetGame()
        {
            BattleCounter = 0;
        }


        public void ResetBattle()
        {
            _currentTurn = 0;
            _turnCounters = new Dictionary<BattleUnit, int>();
        }


        public bool CheckWin()
        {
            return _player.IsDead() || _enemy.IsDead();
        }


        public string GetWinText()
        {
            return _player.IsDead() ? "You Lose" : "You Win";
        }


        public void OnBattleEnd()
        {
            BattleCounter++;
            if (!_player.IsDead())
            {
                OnPlayerWin?.Invoke();
            }
            else
            {
                OnPlayerLose?.Invoke(_enemy);
            }
        }


        private void EnsureCounter(BattleUnit unit)
        {
            _turnCounters.TryAdd(unit, 0);
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

            EnsureCounter(_attackingUnit);
            _turnCounters[_attackingUnit]++;

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
            SwapUnits();

            void OnBeforeHitAbilities(BattleUnit owner)
            {
                context.AbilityOwner = owner;
                foreach (IOnBeforeHitAbility ability in context.AbilityOwner.Abilities.OfType<IOnBeforeHitAbility>())
                {
                    ability.OnBeforeHit(context);
                }
            }
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