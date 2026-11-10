using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HordeProtocol.Data
{
    [CreateAssetMenu(fileName = "newWaveDefenition", menuName = "Enemies/Wave Definition")]
    public class WaveDefenition : ScriptableObject
    {
        [SerializeField] private WaveEnemyDefenition[] enemiesToSpawn;
        public IReadOnlyList<WaveEnemyDefenition> EnemiesToSpawn => enemiesToSpawn;
        [SerializeField] private int waveNumber;
        public int WaveNumber => waveNumber;

        [Serializable]
        public class WaveEnemyDefenition
        {
            [SerializeField] private EnemyDefenition enemyDefenition;
            public EnemyDefenition EnemyDefenition { get { return enemyDefenition; } }
            [SerializeField] private int count;
            public int Count { get { return count; } }

            public WaveEnemyDefenition(EnemyDefenition enemyDefenition, int count)
            {
                this.enemyDefenition = enemyDefenition;
                this.count = count;
            }
        }
    }
}
