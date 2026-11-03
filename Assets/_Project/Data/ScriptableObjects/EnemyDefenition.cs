using UnityEngine;

namespace HordeProtocol.Data
{
    [CreateAssetMenu(fileName = "newEnemyDefenition", menuName = "Enemies/Enemy Defenition")]
    public class EnemyDefenition : ScriptableObject
    {
        [SerializeField] private string name;
        public string Name { get { return name; } }
        [SerializeField] private GameObject enemyPrefab;
        public GameObject EnemyPrefab { get { return enemyPrefab; } }
    }
}
