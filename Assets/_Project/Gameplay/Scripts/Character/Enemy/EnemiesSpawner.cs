using HordeProtocol.Data;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class EnemiesSpawner : MonoBehaviour
    {
        private Transform[] spawnPositions;
        private Transform[] GetSpawnPositions()
        {
            Transform[] positions = new Transform[transform.childCount];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = transform.GetChild(i).transform;
            }

            return positions;
        }

        [SerializeField] private EnemyDefenition enemyDefenition;
        [SerializeField] private float spawnCooldown = 1f;
        private float curSpawnCooldown;

        private EnemiesFactory enemiesFactory;

        private void Awake()
        {
            spawnPositions = GetSpawnPositions();
            curSpawnCooldown = spawnCooldown;

            enemiesFactory = new EnemiesFactory(enemyDefenition);
        }

        private void Update()
        {
            if (curSpawnCooldown > 0f)
            {
                curSpawnCooldown -= Time.deltaTime;
                if (curSpawnCooldown <= 0f)
                {
                    SpawnEnemy();
                    curSpawnCooldown = spawnCooldown;
                }
            }
        }

        private void SpawnEnemy()
        {
            SpawnEnemy(enemyDefenition);
        }

        private void SpawnEnemy(EnemyDefenition enemyDefenition)
        {
            Vector2 spawnPos = GetRandomSpawnPos();
            enemiesFactory.SpawnEnemy(spawnPos);
        }

        private Vector2 GetRandomSpawnPos()
        {
            if (spawnPositions.Length == 0) return transform.position;
            var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
            return spawnPoint.position;
        }
    }
}
