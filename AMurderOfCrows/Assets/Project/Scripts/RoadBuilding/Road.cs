using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using AMarderOfCrows.RoadBuilding;

namespace AMurderOfCrows.RoadBuilding
{

    public class Road : MonoBehaviour
    {
        [SerializeField] private SplineContainer _spline;

        private void OnEnable()
        {
            PointMover.OnPointMoved += UpdateKnotPosition;
        }

        private void OnDisable()
        {
            PointMover.OnPointMoved -= UpdateKnotPosition;
        }

        private void UpdateKnotPosition(int index, Vector2 position)
        {
            if (index <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(index));

            BezierKnot knot = new BezierKnot(new float3(position.x, position.y, 0));
            _spline.Spline.SetKnot(index, knot);
        }

        public void AddPoint(Vector2 position)
        {
            _spline.Spline.Add(new float3(position.x, position.y, 0));
        }

        public void RemovePoint(int index)
        {
            if (index <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(index));

            _spline.Spline.RemoveAt(index);
        }
    }
}
