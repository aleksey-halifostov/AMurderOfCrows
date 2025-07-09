using System;
using UnityEngine;

namespace AMurderOfCrows.RoadBuilding
{
    public class RoadPoint : MonoBehaviour
    {
        private int _index;

        public int Index => _index;

        public static event Action<int> OnPointerRemoved;

        private void OnDestroy()
        {
            OnPointerRemoved?.Invoke(_index);
        }

        public void SetIndex(int index)
        {
            if (index < 0)
                throw new System.ArgumentOutOfRangeException(nameof(index));

            _index = index;
        }
    }
}
