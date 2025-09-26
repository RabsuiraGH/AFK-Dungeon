using System;
using System.Collections.Generic;
using System.Linq;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.AbilitySystem.Interfaces;
using LA.AudioSystem;
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
        public event Action<BattleUnit,bool> OnUnitAttack;

        public event Action<int> OnTurnCountUpdated;

        public event Action OnPlayerWin;
        public event Action<Enemy.Enemy> OnPlayerLose;

        private IRandomService _randomService;
        private SoundFXService _soundFXService;
        private SoundFXDatabase _soundFXDatabase;

        [VContainer.Inject]
        public void Construct(IRandomService randomService, SoundFXService soundFXService, PathConfig pathConfig)
        {
            _randomService = randomService;
            _soundFXDatabase = LoadAssetUtility.Load<SoundFXDatabase>(pathConfig.SoundFXDatabase);
            _soundFXService = soundFXService;
        }


        public void NextTurn()
        {
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

            OnUnitAttack?.Invoke(context.Attacker, isHit);
            _soundFXService.PlayRandomSoundFX(_soundFXDatabase.Attack, Vector2.zero);

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


            OnUnitUpdates?.Invoke(_player);
            OnUnitUpdates?.Invoke(_enemy);

            void OnBeforeHitAbilities(BattleUnit owner)
            {
                context.AbilityOwner = owner;
                foreach (IOnBeforeHitAbility ability in context.AbilityOwner.Abilities.OfType<IOnBeforeHitAbility>())
                {
                    ability.OnBeforeHit(context);
                }
            }
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