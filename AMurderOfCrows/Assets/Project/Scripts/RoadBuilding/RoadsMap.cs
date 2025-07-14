using MurderOfCrows.Crow;
using UnityEngine;
using UnityEngine.Splines;

namespace MurderOfCrows.RoadBuilding
{
    public class RoadsMap : MonoBehaviour
    {
        private Road _currentRoad;
        private RoadMarksContainer _container;

        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private GameObject _firstMark;
        [SerializeField] private Transform _player;

        public bool IsHaveRoad => _currentRoad != null;

        private void Awake()
        {
            _container = new();
        }

        private void OnEnable()
        {
            MarkMover.OnMarkChanged += UpdateMarkPosition;
            CrowMover.OnCrowArrived += DestroyRoad;
        }

        private void OnDisable()
        {
            MarkMover.OnMarkChanged -= UpdateMarkPosition;
            CrowMover.OnCrowArrived += DestroyRoad;
        }

        private void DestroyRoad(Spline _spline)
        {

        }

        private void CreateRoad()
        {
            _splineContainer.AddSpline();
            _currentRoad = new Road(_splineContainer.Splines[_splineContainer.Splines.Count - 1]);
            Mark mark = Instantiate(_firstMark, _player.position, Quaternion.identity).GetComponent<Mark>();
            mark.gameObject.GetComponent<MarkMover>().enabled = false;
            mark.DisableCollider();
            Add(mark);
        }

        private void Add(Mark mark)
        {
            if (mark == null)
                throw new System.ArgumentNullException(nameof(mark));

            mark.Index = _currentRoad.GetRoadLength();
            _currentRoad.AddKnot(mark.transform.position);
            _container.AddMark(mark);
        }

        private void UpdateMarkPosition(int index, Vector3 position)
        {
            if (index < 1 || index >= _currentRoad.GetRoadLength())
                throw new System.ArgumentOutOfRangeException(nameof(index));

            _currentRoad.UpdateKnotPosition(index, position);
        }

        public void AddMark(Mark mark)
        {
            if (mark == null)
                throw new System.ArgumentNullException(nameof(mark));

            if (_currentRoad == null)
                CreateRoad();

            Add(mark);
        }

        public void RemoveMark(int index)
        {
            if (index <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(index));

            _currentRoad.RemoveKnot(index);
            _container.RemoveMark(index);
        }

        public Spline FinishRoad()
        {
            if (!IsHaveRoad)
                throw new System.InvalidOperationException("RoadsMap: The action can not be performed, because Current Road is null");

            Spline road = _currentRoad.GetSpline();
            _currentRoad = null;
            _container.ClearMarks();

            return road;
        }
    }
}