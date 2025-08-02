using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower
{
    private Spline _spline;
    private Transform _transform;

    private Vector3 _lastPosition;

    public SplineFollower(Transform transform, Spline spline)
    {
        _transform = transform;
        _spline = spline;
    }

    public (Vector3 position, Quaternion rotation) Evaluate(float progress)
    {
        _lastPosition = _transform.position;
        Vector3 newPosition = _spline.EvaluatePosition(Mathf.Clamp01(progress));

        Vector3 direction = newPosition - _lastPosition;
        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return (newPosition, Quaternion.Euler(0, 0, angleZ));
    }
}
