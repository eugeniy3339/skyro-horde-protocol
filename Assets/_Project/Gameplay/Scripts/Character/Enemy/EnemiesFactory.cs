using HordeProtocol.Data;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class EnemiesFactory
    {
        private EnemyDefenition _enemyDefenition;
        public EnemyDefenition EnemyDefenition { get => _enemyDefenition; }

        private ObjectPull _enemyPull;

        public GameObject SpawnEnemy(Vector2 pos)
        {
            GameObject enemy = _enemyPull.GetNextObject();
            enemy.name = _enemyDefenition.Name;
            enemy.transform.position = pos;
            enemy.SetActive(true);
            return enemy;
        }

        internal EnemiesFactory(EnemyDefenition enemyDefenition)
        {
            _enemyDefenition = enemyDefenition;
            _enemyPull = ObjectPullsManager.GetPull(enemyDefenition.EnemyPrefab);
        }
    }
}
