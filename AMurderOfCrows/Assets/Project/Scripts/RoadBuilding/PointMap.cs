using UnityEngine;
using System.Collections.Generic;

namespace AMurderOfCrows.RoadBuilding
{
    [RequireComponent(typeof(Road))]
    public class PointMap : MonoBehaviour
    {
        private List<RoadPoint> _points = new List<RoadPoint>();
        private Road _road;

        [SerializeField] private RoadPoint _firstPoint;

        private void Awake()
        {
            _road = GetComponent<Road>();
            AddPoint(_firstPoint);
        }

        private void OnEnable()
        {
            RoadPoint.OnPointerRemoved += RemovePoint;
        }

        private void OnDisable()
        {
            RoadPoint.OnPointerRemoved -= RemovePoint;
        }

        public void AddPoint(RoadPoint point)
        {
            if (point == null)
                throw new System.ArgumentNullException(nameof(point));

            point.SetIndex(_points.Count);

            _road.AddPoint(point.transform.position);
            _points.Add(point);
        }

        public void RemovePoint(int index)
        {
            if (index <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(index));

            _points.RemoveAt(index);
            _road.RemovePoint(index);

            for (int i = index; i < _points.Count; i++)
            {
                _points[i].SetIndex(i);
            }
        }
    }
}
