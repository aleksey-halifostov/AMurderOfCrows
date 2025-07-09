using System;
using AMurderOfCrows.RoadBuilding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AMarderOfCrows.RoadBuilding
{
    [RequireComponent(typeof(RoadPoint))] 
    public class PointMover : MonoBehaviour
    {
        private RoadPoint _point;

        public static event Action<int, Vector2> OnPointMoved;

        private void Awake()
        {
            _point = GetComponent<RoadPoint>();
        }

        private void Update()
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = new Vector2(clickPosition.x, clickPosition.y);
            OnPointMoved?.Invoke(_point.Index, transform.position);
        }
    }
}
