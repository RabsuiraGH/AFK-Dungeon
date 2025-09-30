using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.AbilitySystem.Interfaces;
using LA.AudioSystem;
using LA.AudioSystem.Database;
using LA.BattleLog;
using SW.Utilities.LoadAsset;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LA.Gameplay.GameLoop
{
    [Serializable]
    public class BattleService
    {
        [SerializeField] private Enemy.Enemy _enemy;

        [SerializeField] private Player.Player _player;

        private BattleUnit _attackingUnit;
        private BattleUnit _defendingUnit;

        [SerializeField] private int _currentTurn = 0;
        private Dictionary<BattleUnit, int> _turnCounters = new();

        public event Action<BattleUnit> OnUnitUpdates;
        public event Action<BattleUnit, bool> OnUnitAttack;

        public event Action<int> OnTurnCountUpdated;

        public event Action OnPlayerWin;
        public event Action<Enemy.Enemy> OnPlayerLose;

        private IRandomService _randomService;
        private SoundFXService _soundFXService;
        private SoundFXDatabase _soundFXDatabase;
        private BattleLogService _battleLogService;


        [VContainer.Inject]
        public void Construct(IRandomService randomService, BattleLogService battleLogService,
                              SoundFXService soundFXService, PathConfig pathConfig)
        {
            _randomService = randomService;
            _soundFXDatabase = LoadAssetUtility.Load<SoundFXDatabase>(pathConfig.SoundFXDatabase);
            _soundFXService = soundFXService;
            _battleLogService = battleLogService;
        }


        public void NextTurn()
        {
            AddTurn(_attackingUnit);

            using BattleContext context = CreateBattleContext();
            context.OnInnerLog += (m) => _battleLogService.Log(m, "ABILITY");

            _battleLogService.Log($"Turn {_currentTurn} starts! Attacker: {context.Attacker.Name}", "BATTLE");

            CalculateHit(context);

            OnBeforeHitAbilities(context, context.Attacker);
            OnBeforeHitAbilities(context, context.Defender);

            _battleLogService.Log($"Hit chance: {context.HitChance} ~ {(context.IsHit ? "HIT!" : "MISS")}", "BATTLE");

            PerformAttack(context, out int totalDamage);

            if (totalDamage != 0)
            {
                _battleLogService.Log($"Total damage: {totalDamage}", "BATTLE");
            }

            PerformCounterDamage(context, out int counterDamage);

            if (counterDamage != 0)
            {
                _battleLogService.Log($"Counter damage: {counterDamage}", "BATTLE");
            }

            _battleLogService.LogSeparator();


            UpdateUnits(context);
        }


        private BattleContext CreateBattleContext()
        {
            BattleContext context = new()
            {
                TurnNumber = _currentTurn,
                Attacker = _attackingUnit,
                Defender = _defendingUnit,
                TurnCounters = new Dictionary<BattleUnit, int>(_turnCounters)
            };
            context.Init();

            return context;
        }


        private void CalculateHit(BattleContext context)
        {
            context.HitChance = CalculateHitChance(context.Attacker, context.Defender);


            context.IsHit = context.Defender.IsHit(context.HitChance);
        }


        private void PerformAttack(BattleContext context, out int totalDamage)
        {
            totalDamage = 0;
            OnUnitAttack?.Invoke(context.Attacker, context.IsHit);
            _soundFXService.PlayRandomSoundFX(_soundFXDatabase.Attack, Vector2.zero);

            if (context.IsHit)
            {
                totalDamage = context.GetTotalDamage(context.Attacker);

                if (totalDamage > 0)
                {
                    context.Defender.TakeDamage(totalDamage);
                }
            }
        }


        private void PerformCounterDamage(BattleContext context, out int totalDamage)
        {
            totalDamage = context.GetTotalOtherDamage(context.Defender);
            context.Attacker.TakeDamage(totalDamage);
        }


        private void UpdateUnits(BattleContext context)
        {
            OnUnitUpdates?.Invoke(_player);
            OnUnitUpdates?.Invoke(_enemy);
        }


        private void OnBeforeHitAbilities(BattleContext context, BattleUnit owner)
        {
            context.AbilityOwner = owner;
            foreach (IOnBeforeHitAbility ability in context.AbilityOwner.Abilities.OfType<IOnBeforeHitAbility>())
            {
                ability.OnBeforeHit(context);
            }
        }


        private int CalculateHitChance(BattleUnit attacker, BattleUnit defender)
        {
            return _randomService.Range(1, attacker.Stats.Agility + defender.Stats.Agility + 1);
        }


        public void SetEnemy(Enemy.Enemy enemyBase)
        {
            _enemy = enemyBase;
        }


        public Enemy.Enemy GetEnemy()
        {
            return _enemy;
        }


        public void SetPlayer(Player.Player player)
        {
            _player = player;
            _player.RestoreHealth();
            OnUnitUpdates?.Invoke(_player);
        }


        public void ResetBattle()
        {
            _currentTurn = 0;
            _turnCounters = new Dictionary<BattleUnit, int>();
            OnTurnCountUpdated?.Invoke(_currentTurn);
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
            _currentTurn += 1;
            _turnCounters.TryAdd(unit, 0);
            _turnCounters[_attackingUnit]++;
            OnTurnCountUpdated?.Invoke(_currentTurn);
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