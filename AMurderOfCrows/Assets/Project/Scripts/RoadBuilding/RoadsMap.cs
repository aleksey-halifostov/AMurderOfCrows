using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using MurderOfCrows.Crow;

namespace MurderOfCrows.RoadBuilding
{
    public class RoadsMap : MonoBehaviour, IRoadSource, IRoadContainer
    {
        private Road _currentRoad;
        private RoadMarksContainer _currentContainer;
        private GameActions _input;
        private List<RoadMarksContainer> _containers;

        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private GameObject _firstMark;
        [SerializeField] private Transform _player;

        public bool IsHaveRoad => _currentRoad != null;

        private void Awake()
        {
            _containers = new List<RoadMarksContainer>();
            _input = new GameActions();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.CancelBuilding.performed += context => DestroyCurrentRoad();

            MarkMover.OnMarkChanged += UpdateMarkPosition;
            CrowController.OnCrowArrived += DestroyRoad;
        }

        private void OnDisable()
        {
            MarkMover.OnMarkChanged -= UpdateMarkPosition;
            CrowController.OnCrowArrived -= DestroyRoad;

            _input.Player.CancelBuilding.performed -= context => DestroyCurrentRoad();

            _input.Disable();
        }

        private void DestroyRoad(Spline spline)
        {
            if (spline == null)
                throw new ArgumentNullException(nameof(spline));

            Spline[] splines = _splineContainer.Splines.ToArray();
            int index = Array.IndexOf(splines, spline);

            _containers[index].ClearMarks();
            _containers.RemoveAt(index);
            _splineContainer.RemoveSpline(spline);
        }

        private void DestroyCurrentRoad()
        {
            if (_currentRoad == null)
                return;

            DestroyRoad(_currentRoad.GetSpline());
            _currentRoad = null;
        }

        private void CreateRoad()
        {
            _splineContainer.AddSpline();
            _currentContainer = new RoadMarksContainer();
            _containers.Add(_currentContainer);
            _currentRoad = new Road(_splineContainer.Splines[_splineContainer.Splines.Count - 1]);


            Mark mark = Instantiate(_firstMark, _player.position, Quaternion.identity).GetComponent<Mark>();
            mark.gameObject.GetComponent<MarkMover>().enabled = false;
            mark.DisableCollider();

            Add(mark);
        }

        private void Add(Mark mark)
        {
            if (mark == null)
                throw new ArgumentNullException(nameof(mark));

            mark.Index = _currentRoad.GetRoadLength();
            _currentRoad.AddKnot(mark.transform.position);
            _currentContainer.AddMark(mark);
        }

        private void UpdateMarkPosition(int index, Vector3 position)
        {
            if (index < 1 || index >= _currentRoad.GetRoadLength())
                throw new ArgumentOutOfRangeException(nameof(index));

            _currentRoad.UpdateKnotPosition(index, position);
        }

        public void AddMark(Mark mark)
        {
            if (mark == null)
                throw new ArgumentNullException(nameof(mark));

            if (_currentRoad == null)
                CreateRoad();

            Add(mark);
        }

        public void RemoveMark(int index)
        {
            if (index <= 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            _currentRoad.RemoveKnot(index); 
            _currentContainer.RemoveMark(index);
        }

        public Spline GetRoad()
        {
            if (!IsHaveRoad)
                throw new InvalidOperationException("RoadsMap: The action can not be performed, because Current Road is null");

            Spline road = _currentRoad.GetSpline();
            _currentContainer.SetupMarks();
            _currentRoad = null;

            return road;
        }
    }
}