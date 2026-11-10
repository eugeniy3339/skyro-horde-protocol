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

        [SerializeField] private WaveDefenition _testWaveDefenition;

        private void Awake()
        {
            spawnPositions = GetSpawnPositions();
            SpawnWave(_testWaveDefenition);
        }

        private void SpawnWave(WaveDefenition waveDefenition)
        {
            foreach (var waveEnemies in waveDefenition.EnemiesToSpawn)
            {
                SpawnWaveEnemies(waveEnemies);
            }
        }

        private void SpawnWaveEnemies(WaveDefenition.WaveEnemyDefenition waveEnemies)
        {
            EnemiesFactory enemiesFactory = new EnemiesFactory(waveEnemies.EnemyDefenition);
            for (int i = 0; i < waveEnemies.Count; i++)
            {
                enemiesFactory.SpawnEnemy(GetRandomSpawnPos());
            }
        }

        private Vector2 GetRandomSpawnPos()
        {
            if (spawnPositions.Length == 0) return transform.position;
            var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
            return spawnPoint.position;
        }
    }
}
