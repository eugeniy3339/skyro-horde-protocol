using HordeProtocol.Data;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class EnemiesFactory
    {
        private EnemyDefenition _enemyDefenition;
        public EnemyDefenition EnemyDefenition { get => _enemyDefenition; }

        private ObjectPull<Enemy> _enemyPull;

        public Enemy SpawnEnemy(Vector2 pos)
        {
            Enemy enemy = _enemyPull.GetNextObject();
            enemy.name = _enemyDefenition.Name;
            enemy.transform.position = pos;
            enemy.gameObject.SetActive(true);
            return enemy;
        }

        internal EnemiesFactory(EnemyDefenition enemyDefenition)
        {
            _enemyDefenition = enemyDefenition;
            _enemyPull = ObjectPullsManager.GetPull<Enemy>(enemyDefenition.EnemyPrefab);
        }
    }
}
