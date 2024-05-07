using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField] private Camera _worldCamera;
    [SerializeField] private GameObject _pointPrefab;

    private Vector2 _initialPosition;
    private Vector2 _initialDirection;
    private float _force = 20f;
    private int _pointsCount = 20;
    private GameObject[] _points;

    private void Start()
    {
        _points = new GameObject[_pointsCount];
        for (int i = 0; i < _pointsCount; i++)
        {
            _points[i] = Instantiate(_pointPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector2 arrowPosition = gameObject.transform.position;
        Vector2 arrowDirection = gameObject.transform.right.normalized;

        if (Input.GetMouseButton(0))
        {
            DisplayTrajectory(arrowPosition, arrowDirection);
        }
        else
        {
            HideTrajectory();
        }
    }

    private void DisplayTrajectory(Vector2 initialPosition, Vector2 initialDirection)
    {
        for (int i = 0; i < _pointsCount; i++)
        {
            float t = i / (float)(_pointsCount - 1);
            Vector2 position = CalculatePointPosition(initialPosition, initialDirection, t);
            _points[i].transform.position = position;
        }
    }

    private void HideTrajectory()
    {
        for (int i = 0; i < _pointsCount; i++)
        {
            _points[i].transform.position = transform.position;
        }
    }

    private Vector2 CalculatePointPosition(Vector2 initialPosition, Vector2 initialDirection, float t)
    {
        Vector2 currentPosition = initialPosition +
                                  (initialDirection * _force * t) +
                                  0.5f * Physics2D.gravity * (t * t);
        return currentPosition;
    }
}