using UnityEngine;
using UnityEngine.InputSystem;
using MurderOfCrows.RoadBuilding;

namespace MurderOfCrows.Player
{
    public class PointBuilder : MonoBehaviour
    {
        private GameActions _input;
        private MarkMover _currentPoint;

        private IRoadContainer Map => _roadMap;

        [SerializeField] private RoadsMap _roadMap;
        [SerializeField] private GameObject _pointPrefab;


        private void Awake()
        {
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

            if (collider == null || !collider.TryGetComponent<MarkMover>(out _currentPoint))
            {
                _currentPoint = Instantiate(_pointPrefab, new Vector2(clickPosition.x, clickPosition.y), Quaternion.identity).GetComponent<MarkMover>();
                Map.AddMark(_currentPoint.GetComponent<Mark>());
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);

            if (hit.collider == null || !hit.collider.TryGetComponent<Mark>(out Mark mark))
                return;

            Map.RemoveMark(mark.Index);
            Destroy(hit.collider.gameObject);
        }
    }
}
