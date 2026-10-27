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

        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnCooldown = 1f;
        private float curSpawnCooldown;

        private void Awake()
        {
            spawnPositions = GetSpawnPositions();

            curSpawnCooldown = spawnCooldown;
        }

        private void Update()
        {
            if(curSpawnCooldown > 0f)
            {
                curSpawnCooldown -= Time.deltaTime;
                if (curSpawnCooldown <= 0f)
                    SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            SpawnEnemy(enemyPrefab);
        }

        private void SpawnEnemy(GameObject enemyPrefab)
        {
            Vector2 spawnPos = GetRandomSpawnPos();
        }

        private Vector2 GetRandomSpawnPos()
        {
            if (spawnPositions.Length == 0) return transform.position;
            var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
            return spawnPoint.position;
        }
    }
}
