using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using MurderOfCrows.Enemies;

namespace MurderOfCrows.EnemySpawn
{
    [RequireComponent(typeof(EnemyPool))]
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyPool _pool;

        [SerializeField] private SplineContainer _container;

        private void Awake()
        {
            _pool = GetComponent<EnemyPool>();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                float wait = 2.5f;
                int index = Random.Range(0, _container.Splines.Count);
                SpawnEnemy(_container.Splines[index], _pool.GetEnemy());
                yield return new WaitForSeconds(wait);
            }
        }

        private void SpawnEnemy(Spline spline, GameObject enemy)
        {
            enemy.GetComponent<Enemy>().Init(spline);
            enemy.SetActive(true);
        }
    }
}