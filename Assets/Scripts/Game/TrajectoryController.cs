using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _pointPrefab;

    [SerializeField]
    private Transform _shotPoint;

    [SerializeField]
    private int _pointsCount = 20;

    [SerializeField]
    private float _launchForce;

    [SerializeField]
    private float _spaceBetweenPoints;

    [SerializeField]
    private Transform _trajectoryRoot;

    private GameObject[] _points;
    private Vector2 _direction;
    
    private void Start()
    {
        CreatePoints();
        HideTrajectory();
    }

    private void LateUpdate()
    {
        Vector2 shotPoint = _shotPoint.position;
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _direction = mousePosition - shotPoint;
        _shotPoint.right = _direction;
        if (Input.GetMouseButton(0) && IsWithinAngleLimit())
        {
            ShowTrajectory();
        }
        else
        {
            HideTrajectory();
        }
    }

    private bool IsWithinAngleLimit()
    {
        Vector2 baseDirection = Vector2.right;
        float angle = Vector2.Angle(baseDirection, _direction);
        return angle <= 90.0f;
    }

    private void ShowTrajectory()
    {
        for (int i = 0; i < _pointsCount; i++)
        {
            _points[i].SetActive(true);
            _points[i].transform.position = CalculatePointPosition(i * _spaceBetweenPoints);
        }
    }

    private void HideTrajectory()
    {
        foreach (var point in _points)
        {
            point.SetActive(false);
        }
    }

    private void CreatePoints()
    {
        _points = new GameObject[_pointsCount];
        for (int i = 0; i < _pointsCount; i++)
        {
            _points[i] = Instantiate(_pointPrefab, _shotPoint.position, Quaternion.identity);
            _points[i].SetActive(false);
            _points[i].transform.SetParent(_trajectoryRoot);
        }
    }

    private Vector2 CalculatePointPosition(float t)
    {
        Vector2 position = (Vector2)_shotPoint.position +
                           (_direction.normalized * (_launchForce * t)) +
                           Physics2D.gravity * (0.5f * (t * t));
        return position;
    }
}