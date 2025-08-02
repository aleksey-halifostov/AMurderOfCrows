using UnityEngine;
using System.Collections.Generic;

namespace MurderOfCrows.Effects
{
    public class DestroyEffetcsPool : MonoBehaviour
    {
        private List<ParticleSystem> _effects = new List<ParticleSystem>();

        [SerializeField] private ParticleSystem _prefab;

        private ParticleSystem Create()
        {
            ParticleSystem effect = Instantiate(_prefab);
            _effects.Add(effect);

            return effect;
        }

        public ParticleSystem GetEffect()
        {
            foreach (ParticleSystem effect in _effects)
            {
                if (!effect.isPlaying)
                {
                    return effect;
                }
            }

            return Create();
        }
    }
}