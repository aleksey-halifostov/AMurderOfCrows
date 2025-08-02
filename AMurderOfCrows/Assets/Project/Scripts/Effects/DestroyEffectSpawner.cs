using UnityEngine;

namespace MurderOfCrows.Effects
{
    [RequireComponent(typeof(DestroyEffetcsPool))]
    public class DestroyEffectSpawner : MonoBehaviour
    {
        private DestroyEffetcsPool _pool;

        private void Awake()
        {
            _pool = GetComponent<DestroyEffetcsPool>();
        }

        private void OnEnable()
        {
            DestroyEffect.OnEffectActivated += ActivateDestroyEffect;
        }

        private void OnDisable()
        {
            DestroyEffect.OnEffectActivated -= ActivateDestroyEffect;
        }

        private void ActivateDestroyEffect(Vector3 position)
        {
            ParticleSystem effect = _pool.GetEffect();
            effect.transform.position = position;
            effect.Play();
        }
    }
}
