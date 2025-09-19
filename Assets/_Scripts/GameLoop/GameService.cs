using System;
using SW.Utilities.LoadAsset;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

namespace LA
{
    [Serializable]
    public class GameService
    {
        [field: SerializeField] public int BattleCounter { get; private set; } = 0;

        private EnemyDatabase _enemyDatabase;
        [field: SerializeField] public BattleService BattleService { get; private set; }

        [SerializeField] private float _baseTurnIntervalInSeconds = 1f;
        [SerializeField] private float _gameSpeed = 1f;

        private Player _player;
        private GameplayConfig _gameplayConfig;


        public void ResetGame()
        {
            BattleCounter = 0;
        }


        [VContainer.Inject]
        public void Construct(Player player, BattleService battleService, PathConfig pathConfig)
        {
            _player = player;
            _player.Init();

            BattleService = battleService;
            BattleService.OnPlayerWin += CountWin;

            _enemyDatabase = LoadAssetUtility.Load<EnemyDatabase>(pathConfig.EnemyDatabase);
            _gameplayConfig = LoadAssetUtility.Load<GameplayConfig>(pathConfig.GameplayConfig);

            _baseTurnIntervalInSeconds = _gameplayConfig.BaseTurnIntervalInSeconds;
            _gameSpeed = _gameplayConfig.GameSpeeds[0];
        }


        private void CountWin()
        {
            BattleCounter++;
            if (BattleCounter >= 5)
            {
                Debug.Log(($"Game completed"));
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
            BattleService.SetEnemy(GetRandomEnemy());
            BattleService.SetPlayer(_player);


            BattleService.DecideFirstTurn();

            while (!token.IsCancellationRequested)
            {
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


        private Enemy GetRandomEnemy()
        {
            EnemySO enemyBase = _enemyDatabase.GetRandomEnemy();

            if (enemyBase == null)
            {
                throw new NullReferenceException("Enemy database is empty!");
            }

            Enemy enemy = new Enemy(enemyBase);
            enemy.Init();
            return enemy;
        }
    }
}