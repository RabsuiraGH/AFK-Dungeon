using SW.Utilities.LoadAsset;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

namespace LA.UI
{
    public class MainLoopSim : MonoBehaviour
    {
        [SerializeField] private EnemyDatabase _enemyDatabase;

        [SerializeField] private MainGameLoop _mainGameLoop;
        private Player _player;


        [VContainer.Inject]
        public void Construct(Player player, MainGameLoop mainGameLoop, PathConfig pathConfig)
        {
            _player = player;
            _player.Init();

            _mainGameLoop = mainGameLoop;
            _enemyDatabase = LoadAssetUtility.Load<EnemyDatabase>(pathConfig.EnemyDatabase);
        }


        [EasyButtons.Button]
        public void OneClickStartBattle()
        {
            _mainGameLoop.ResetBattle();
            _player.RestoreHealth();
            AddRandomEnemy();
            using CancellationTokenSource cts = new CancellationTokenSource();
            SimulateBattle(cts.Token);
        }


        public async Task SimulateBattle(CancellationToken token)
        {
            while (!_mainGameLoop.CheckWin() && !token.IsCancellationRequested)
            {
                _mainGameLoop.SimulateTurn();
                if (_mainGameLoop.CheckWin())
                {
                    break;
                }

                await Task.Delay(1000, token);
            }

            Debug.Log(($"{_mainGameLoop.GetWinText()}"));
        }


        [EasyButtons.Button]
        public void AddRandomEnemy()
        {
            EnemySO enemy = _enemyDatabase.GetRandomEnemy();
            if (enemy == null)
            {
                return;
            }

            _mainGameLoop.CreateEnemy(enemy);
        }


        [EasyButtons.Button]
        public void AddSpecificEnemy(EnemySO enemy)
        {
            _mainGameLoop.CreateEnemy(enemy);
        }


        [EasyButtons.Button]
        public void Reset()
        {
            _mainGameLoop.ResetBattle();
        }
    }
}