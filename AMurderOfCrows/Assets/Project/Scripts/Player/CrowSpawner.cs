using UnityEngine;
using MurderOfCrows.Crow;
using MurderOfCrows.RoadBuilding;

namespace MurderOfCrows.Player
{
    public class CrowSpawner : MonoBehaviour
    {
        private GameActions _input;

        private IRoadSource Source => _roadsMap;

        [SerializeField] private GameObject _crowPrefab;
        [SerializeField] private RoadsMap _roadsMap;


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
            if (!Source.IsHaveRoad)
                return;

            CrowController crow = Instantiate(_crowPrefab).GetComponent<CrowController>();
            crow.Init(Source.GetRoad());
        }
    }
}