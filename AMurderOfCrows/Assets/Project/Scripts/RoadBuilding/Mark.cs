using UnityEngine;

namespace MurderOfCrows.RoadBuilding
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Mark : MonoBehaviour
    {
        private CircleCollider2D _collider;
        public int _index;

        public int Index
        {
            get { return _index; }
            set 
            {
                if (value < 0)
                    throw new System.ArgumentOutOfRangeException(nameof(value));

                _index = value;
            }
        }

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
        }
    }
}