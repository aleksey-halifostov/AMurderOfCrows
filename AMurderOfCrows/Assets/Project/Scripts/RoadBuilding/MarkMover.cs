using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MurderOfCrows.RoadBuilding
{
    [RequireComponent(typeof(Mark))]
    public class MarkMover : MonoBehaviour
    {
        private Mark mark;

        public static event Action<int, Vector3> OnMarkChanged;

        private void Awake()
        {
            mark = GetComponent<Mark>();
        }

        private void Update()
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            OnMarkChanged?.Invoke(mark.Index, transform.position);
        }
    }
}