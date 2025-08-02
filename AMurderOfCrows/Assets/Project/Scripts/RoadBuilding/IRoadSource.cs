using UnityEngine.Splines;

namespace MurderOfCrows.RoadBuilding
{
    public interface IRoadSource
    {
        public bool IsHaveRoad { get; }

        public Spline GetRoad();
    }
}