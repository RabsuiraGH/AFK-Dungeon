using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LA
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Game/EnemyDatabase")]
    public class EnemyDatabase : ScriptableObject
    {
        [SerializeField] private List<EnemySO> _enemies = new();


        public EnemySO GetRandomEnemy()
        {
            if (!_enemies.Any())
            {
                return null;
            }

            return _enemies[Random.Range(0, _enemies.Count)];
        }
    }
}