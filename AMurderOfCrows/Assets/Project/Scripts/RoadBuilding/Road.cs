using UnityEngine;
using UnityEngine.Splines;

namespace MurderOfCrows.RoadBuilding
{
    public class Road
    {
        private Spline _spline;

        public Road(Spline spline)
        {
            if (spline == null)
                throw new System.ArgumentNullException(nameof(spline));

            _spline = spline;
        }

        public void UpdateKnotPosition(int index, Vector3 position)
        {
            _spline[index] = new BezierKnot(position);
        }

        public void AddKnot(Vector3 position)
        {
            _spline.Add(position);
        }

        public void RemoveKnot(int index)
        {
            _spline.RemoveAt(index);
        }

        public Spline GetSpline() {  return _spline; }

        public int GetRoadLength() { return _spline.Count; }
    }
}