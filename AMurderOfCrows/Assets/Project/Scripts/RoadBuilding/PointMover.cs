using UnityEngine;
using UnityEngine.InputSystem;

public class PointMover : MonoBehaviour
{
    private void Update()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector2(clickPosition.x, clickPosition.y);
    }
}
