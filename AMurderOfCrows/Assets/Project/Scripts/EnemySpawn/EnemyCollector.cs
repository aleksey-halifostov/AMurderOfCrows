using MurderOfCrows.Enemies;
using UnityEngine;

namespace MurderOfCrows.EnemySpawn
{
    [RequireComponent(typeof(EnemyPool))]
    public class EnemyCollector : MonoBehaviour
    {
        private EnemyPool _pool;

        private void Awake()
        {
            _pool = GetComponent<EnemyPool>();
        }

        private void OnEnable()
        {
            Enemy.OnEnemyDestroyed += CollectEnemy;
        }

        private void OnDisable()
        {
            Enemy.OnEnemyDestroyed -= CollectEnemy;
        }

        private void CollectEnemy(GameObject enemy)
        {
            _pool.AddEnemy(enemy);
        }
    }
}