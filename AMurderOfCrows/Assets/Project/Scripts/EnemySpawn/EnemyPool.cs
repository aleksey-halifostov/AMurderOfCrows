using UnityEngine;
using System.Collections.Generic;

namespace MurderOfCrows.EnemySpawn
{
    public class EnemyPool : MonoBehaviour
    {
        private Queue<GameObject> _pool = new Queue<GameObject>();

        [SerializeField] private GameObject _prefab;

        private GameObject Create()
        {
            return Instantiate(_prefab);
        }

        public void AddEnemy(GameObject enemy)
        {
            _pool.Enqueue(enemy);
        }

        public GameObject GetEnemy()
        {
            if (_pool.Count > 0)
                return _pool.Dequeue();

            return Create();
        }
    }
}
