using System;
using UnityEngine;

namespace MurderOfCrows.Effects
{
    public class DestroyEffect : MonoBehaviour
    {
        public static event Action<Vector3> OnEffectActivated;

        public void Activate()
        {
            OnEffectActivated?.Invoke(transform.position);
        }
    }
}
