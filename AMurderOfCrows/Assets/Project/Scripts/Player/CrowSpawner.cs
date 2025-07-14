using UnityEngine;
using MurderOfCrows.Crow;
using MurderOfCrows.RoadBuilding;

namespace MurderOfCrows.Player
{
    public class CrowSpawner : MonoBehaviour
    {
        private GameActions _input;

        [SerializeField] private GameObject _crowPrefab;
        [SerializeField] private RoadsMap _map;

        private void Awake()
        {
            _input = new GameActions();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Shoot.performed += context => Shoot();
        }

        private void OnDisable()
        {
            _input.Player.Shoot.performed -= context => Shoot();
            _input.Disable();
        }

        private void Shoot()
        {
            if (!_map.IsHaveRoad)
                return;

            CrowMover crow = Instantiate(_crowPrefab).GetComponent<CrowMover>();
            crow.Init(_map.FinishRoad());
        }
    }
}