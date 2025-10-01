using System;
using SW.Utilities.LoadAsset;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using LA.BattleLog;
using LA.Gameplay.Config;
using LA.Gameplay.Enemy;

namespace LA.Gameplay.GameLoop
{
    [Serializable]
    public class GameService : IDisposable
    {
        private int _battleCounter;

        [SerializeField] private float _baseTurnIntervalInSeconds = 1f;
        [SerializeField] private float _gameSpeed = 1f;

        public event Action<Enemy.Enemy> OnPlayerWinBattle;
        public event Action<Enemy.Enemy> OnPlayerLoseBattle;
        public event Action OnPlayerCompletedGame;
        public event Action<BattleUnit> OnUnitUpdates;
        public event Action<BattleUnit, bool> OnUnitAttack;
        public event Action<BattleUnit> OnEnemySet;
        public event Action<int> OnBattleCounterChanged;
        public event Action<int> OnTurnCountUpdated;

        private Player.Player _player;
        private GameplayConfig _gameplayConfig;
        private BattleLogService _battleLogService;
        private EnemyDatabase _enemyDatabase;
        private BattleService _battleService;
        private TaskCompletionSource<bool> _pauseTcs;
        private CancellationTokenSource _cts;


        [VContainer.Inject]
        public void Construct(Player.Player player, BattleService battleService, BattleLogService battleLogService,
                              PathConfig pathConfig)
        {
            _player = player;
            _player.Init();

            _battleService = battleService;
            _battleService.OnPlayerWin += OnWin;
            _battleService.OnPlayerLose += OnLose;
            _battleService.OnUnitUpdates += UpdateUnit;
            _battleService.OnUnitAttack += UnitAttack;
            _battleService.OnTurnCountUpdated += UpdateTurnCount;

            _battleLogService = battleLogService;

            _enemyDatabase = LoadAssetUtility.Load<EnemyDatabase>(pathConfig.EnemyDatabase);
            _gameplayConfig = LoadAssetUtility.Load<GameplayConfig>(pathConfig.GameplayConfig);

            _baseTurnIntervalInSeconds = _gameplayConfig.BaseTurnIntervalInSeconds;
            _gameSpeed = _gameplayConfig.GameSpeeds[0];
        }


        public void StartBattle()
        {
            OnBattleCounterChanged?.Invoke(_battleCounter);
            _cts = new CancellationTokenSource();
            _battleLogService.ClearLog();
            _ = SimulateBattle(_cts.Token);
        }


        public void ResetGame()
        {
            _battleCounter = 0;
        }


        public void SetGameSpeed(float gameSpeed) => _gameSpeed = gameSpeed;

        public float GetTurnDuration() => _baseTurnIntervalInSeconds / _gameSpeed;

        public void PauseBattle() => _pauseTcs ??= new TaskCompletionSource<bool>();


        public void ResumeBattle()
        {
            if (_pauseTcs != null)
            {
                _pauseTcs.TrySetResult(true);
                _pauseTcs = null;
            }
        }


        private async Task SimulateBattle(CancellationToken token)
        {
            await Task.Yield(); // To avoid same frame win/lose

            _battleService.ResetBattle();
            _battleService.SetPlayer(_player);
            Enemy.Enemy enemy = GetRandomEnemy();
            _battleService.SetEnemy(enemy);
            OnEnemySet?.Invoke(enemy);
            _battleService.DecideFirstTurn();

            await Task.Delay(TimeSpan.FromSeconds(0.33f), token);

            while (!token.IsCancellationRequested && !_battleService.CheckBattleEnd())
            {
                await WaitIfPaused(token);

                _battleService.NextTurn();

                if (_battleService.CheckBattleEnd())
                {
                    _battleService.OnBattleEnd();
                    break;
                }

                _battleService.SwapUnits();

                await Task.Delay(TimeSpan.FromSeconds(_baseTurnIntervalInSeconds / _gameSpeed), token);
            }
        }


        private async Task WaitIfPaused(CancellationToken token)
        {
            if (_pauseTcs != null)
            {
                var tcs = _pauseTcs;
                await Task.WhenAny(tcs.Task, Task.Delay(Timeout.Infinite, token));
            }
        }


        private void UpdateTurnCount(int counter) => OnTurnCountUpdated?.Invoke(counter);

        private void UpdateUnit(BattleUnit unit) => OnUnitUpdates?.Invoke(unit);

        private void UnitAttack(BattleUnit unit, bool isHit) => OnUnitAttack?.Invoke(unit, isHit);

        private void OnLose(Enemy.Enemy killedBy) => OnPlayerLoseBattle?.Invoke(killedBy);


        private void OnWin(Enemy.Enemy defeatedEnemy)
        {
            _battleCounter++;
            if (_battleCounter >= 5)
            {
                OnPlayerCompletedGame?.Invoke();
            }
            else
            {
                OnPlayerWinBattle?.Invoke(defeatedEnemy);
            }
        }


        private Enemy.Enemy GetRandomEnemy()
        {
            EnemySO enemyBase = _enemyDatabase.GetRandomEnemy();

            if (enemyBase == null)
            {
                throw new NullReferenceException("Enemy database is empty!");
            }

            Enemy.Enemy enemy = new Enemy.Enemy(enemyBase);
            enemy.Init();
            return enemy;
        }


        public void Dispose()
        {
            _cts?.Cancel();
            _battleService.OnPlayerWin -= OnWin;
            _battleService.OnPlayerLose -= OnLose;
            _battleService.OnUnitUpdates -= UpdateUnit;
            _battleService.OnUnitAttack -= UnitAttack;
            _battleService.OnTurnCountUpdated -= UpdateTurnCount;
        }
    }
}