using System;
using UnityEngine;
using UnityEngine.Splines;

namespace MurderOfCrows.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private Spline _spline;
        private float _speed = 2;
        private SplineFollower _follower;
        private float _progress;
        private float _movementCoeficient;

        public static event Action<GameObject> OnEnemyDestroyed;

        private void Update()
        {
            _progress += _speed * _movementCoeficient * Time.deltaTime;

            if (_progress >= 1f)
                gameObject.SetActive(false);

            Move();
        }

        private void Move()
        {
            var current = _follower.Evaluate(_progress);
            transform.position = current.position;
            transform.rotation = current.rotation;
        }

        private void OnDisable()
        {
            OnEnemyDestroyed?.Invoke(gameObject);
        }

        public void Init(Spline spline)
        {
            _spline = spline;
            _progress = 0;
            _movementCoeficient = 1 / _spline.GetLength();
            _follower = new SplineFollower(transform, _spline);
        }

        public void TakeDamage()
        {
            gameObject.SetActive(false);
        }
    }
}
