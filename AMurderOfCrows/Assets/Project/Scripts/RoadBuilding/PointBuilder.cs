using AMurderOfCrows.RoadBuilding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AMarderOfCrows.RoadBuilding
{
    [RequireComponent(typeof(PointMap))]
    public class PointBuilder : MonoBehaviour
    {
        private GameActions _input;
        private PointMover _currentPoint;
        private PointMap _map;

        [SerializeField] private GameObject _pointPrefab;

        private void Awake()
        {
            _map = GetComponent<PointMap>();
            _input = new GameActions();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.PlacePoint.performed += context => GetPoint();
            _input.Player.PlacePoint.canceled += context => PlacePoint();
            _input.Player.DeletePoint.performed += context => DeletePoint();
        }

        private void OnDisable()
        {
            _input.Player.PlacePoint.performed -= context => GetPoint();
            _input.Player.PlacePoint.canceled -= context => PlacePoint();
            _input.Player.DeletePoint.performed -= context => DeletePoint();
            _input.Disable();
        }

        private void GetPoint()
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D collider = Physics2D.Raycast(clickPosition, Vector2.zero).collider;

            if (collider == null || !collider.TryGetComponent<PointMover>(out _currentPoint))
            {
                _currentPoint = Instantiate(_pointPrefab, new Vector2(clickPosition.x, clickPosition.y), Quaternion.identity).GetComponent<PointMover>();
                _map.AddPoint(_currentPoint.GetComponent<RoadPoint>());
            }

            _currentPoint.enabled = true;
        }

        private void PlacePoint()
        {
            _currentPoint.enabled = false;
            _currentPoint = null;
        }

        private void DeletePoint()
        {
            Collider2D collider = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero).collider;

            if (collider == null || !collider.TryGetComponent<PointMover>(out _currentPoint))
                return;

            Destroy(_currentPoint.gameObject);
            _currentPoint = null;
        }
    }
}
