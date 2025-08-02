using System;
using UnityEngine;
using UnityEngine.Splines;
using MurderOfCrows.Crow.SM;
using MurderOfCrows.Enemies;
using MurderOfCrows.Effects;

namespace MurderOfCrows.Crow
{
    [RequireComponent(typeof(DestroyEffect))]
    public class CrowController : MonoBehaviour
    {
        public Ray _ray;

        private CrowState _state;
        private Spline _spline;
        private DestroyEffect _destroyEffect;

        [SerializeField, Min(0.01f)] private float _speed = 3;

        public float Speed => _speed;

        public static event Action<Spline> OnCrowArrived;

        private void Awake()
        {
            _destroyEffect = GetComponent<DestroyEffect>();
        }

        private void Update()
        {
            _state.Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_spline == null) return;

            if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            {
                OnCrowArrived?.Invoke(_spline);
                SetState(new CrowChasigState(this, enemy.transform));
                _spline = null;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Burst();

            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage();
            } 
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawRay(transform.position, transform.right);
        }

        public void Init(Spline spline)
        {
            if (spline == null)
                throw new ArgumentNullException(nameof(spline));

            _spline = spline;
            SetState(new CrowMoveState(this, _spline));
        }

        public void SetState(CrowState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            _state?.Exit();
            _state = state;
            _state.Enter();
        }

        public void Burst()
        {
            if (_spline != null)
                OnCrowArrived?.Invoke(_spline);

            _destroyEffect.Activate();
            Destroy(gameObject);
        }
    }
}