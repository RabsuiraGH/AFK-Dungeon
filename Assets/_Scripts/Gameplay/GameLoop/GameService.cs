using System;
using SW.Utilities.LoadAsset;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using LA.Gameplay.Config;
using LA.Gameplay.Enemy;

namespace LA.Gameplay.GameLoop
{
    [Serializable]
    public class GameService
    {
        [field: SerializeField] public int BattleCounter { get; private set; } = 0;

        private EnemyDatabase _enemyDatabase;
        [field: SerializeField] public BattleService BattleService { get; private set; }

        [SerializeField] private float _baseTurnIntervalInSeconds = 1f;
        [SerializeField] private float _gameSpeed = 1f;

        public event Action OnPlayerWinBattle;
        public event Action<Enemy.Enemy> OnPlayerLoseBattle;
        public event Action OnPlayerCompletedGame;

        public event Action<BattleUnit> OnEnemySet;


        private Player.Player _player;
        private GameplayConfig _gameplayConfig;

        private TaskCompletionSource<bool> _pauseTcs;


        public void ResetGame()
        {
            BattleCounter = 0;
        }


        public void SetGameSpeed(float gameSpeed) => _gameSpeed = gameSpeed;


        public void PauseBattle()
        {
            if (_pauseTcs == null)
            {
                _pauseTcs = new TaskCompletionSource<bool>();
            }
        }


        public void ResumeBattle()
        {
            if (_pauseTcs != null)
            {
                _pauseTcs.TrySetResult(true);
                _pauseTcs = null;
            }
        }


        [VContainer.Inject]
        public void Construct(Player.Player player, BattleService battleService, PathConfig pathConfig)
        {
            _player = player;
            _player.Init();

            BattleService = battleService;
            BattleService.OnPlayerWin += CountWin;
            BattleService.OnPlayerLose += OnLose;

            _enemyDatabase = LoadAssetUtility.Load<EnemyDatabase>(pathConfig.EnemyDatabase);
            _gameplayConfig = LoadAssetUtility.Load<GameplayConfig>(pathConfig.GameplayConfig);

            _baseTurnIntervalInSeconds = _gameplayConfig.BaseTurnIntervalInSeconds;
            _gameSpeed = _gameplayConfig.GameSpeeds[0];
        }


        private void OnLose(Enemy.Enemy killedBy)
        {
            OnPlayerLoseBattle?.Invoke(killedBy);
        }


        private void CountWin()
        {
            BattleCounter++;
            if (BattleCounter >= 5)
            {
                OnPlayerCompletedGame?.Invoke();
            }
            else
            {
                OnPlayerWinBattle?.Invoke();
            }
        }


        public void StartBattle()
        {
            using CancellationTokenSource cts = new CancellationTokenSource();
            _ = SimulateBattle(cts.Token);
        }


        private async Task SimulateBattle(CancellationToken token)
        {
            await Task.Yield(); // To avoid same frame win/lose

            BattleService.ResetBattle();
            BattleService.SetPlayer(_player);
            Enemy.Enemy enemy = GetRandomEnemy();
            BattleService.SetEnemy(enemy);
            OnEnemySet?.Invoke(enemy);
            BattleService.DecideFirstTurn();

            await Task.Delay(TimeSpan.FromSeconds(0.33f), token);

            while (!token.IsCancellationRequested && !BattleService.CheckBattleEnd())
            {
                await WaitIfPaused(token);

                BattleService.NextTurn();

                if (BattleService.CheckBattleEnd())
                {
                    BattleService.OnBattleEnd();
                    break;
                }

                BattleService.SwapUnits();

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


        ~GameService()
        {
            BattleService.OnPlayerWin -= CountWin;
        }
    }
}