using System;
using UnityEngine;
using UnityEngine.Splines;

namespace MurderOfCrows.Crow
{
    public class CrowMover : MonoBehaviour
    {
        private float _speed = 1;
        private Vector3 _previousPosition = Vector3.zero;
        private Spline _spline;
        private float _ratio;

        [SerializeField] private ParticleSystem _burstEffect;

        public static event Action<Spline> OnCrowArrived;

        private void Update()
        {
            Move();
            Rotate();

            if (_ratio >= 1)
                Burst();
        }

        private void Burst()
        {
            Destroy(gameObject);
            OnCrowArrived?.Invoke(_spline);
            Instantiate(_burstEffect, transform.position, Quaternion.identity);
        }

        private void Move()
        {
            _previousPosition = transform.position;
            transform.position = _spline.EvaluatePosition(_ratio);
            _ratio += Time.deltaTime * .3f;
        }

        private void Rotate()
        {
            Vector3 direction = transform.position - _previousPosition;

            if (direction == Vector3.zero)
                return;

            float angleZ = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angleZ);
        }

        public void Init(Spline spline)
        {
            _spline = spline;
            _ratio = 0;
        }
    }
}