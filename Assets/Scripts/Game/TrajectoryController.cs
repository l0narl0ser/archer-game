using UnityEngine;
using UnityEngine.Serialization;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointPrefab;
    
    [SerializeField]
    private int _pointsCount = 20;
    
    [SerializeField]
    private float _spaceBetweenPoints;

    [SerializeField]
    private GameObject _dotsRoot;

    private Transform[] _points;
    private Vector2 _direction;
    private float _timeStamp;
    private Vector2 pos;

    private void Start()
    {
        CreatePoints();
        HideTrajectory();
    }
    

    public void ShowTrajectory()
    {
        _dotsRoot.SetActive(true);
    }

    public void HideTrajectory()
    {
        _dotsRoot.SetActive(false);
    }

    private void CreatePoints()
    {
        _points = new Transform[_pointsCount];
        for (int i = 0; i < _pointsCount; i++)
        {
            _points[i] = Instantiate(_pointPrefab, null).transform;

            _points[i].SetParent(_dotsRoot.transform);
        }
    }
    

    public void UpdateDots(Vector3 shotPosition, Vector2 forceApplied)
    {
        _timeStamp = _spaceBetweenPoints;
        for (int i = 0; i < _pointsCount; i++)
        {
            pos.x = shotPosition.x + forceApplied.x * _timeStamp;
            pos.y = shotPosition.y + forceApplied.y * _timeStamp -
                    (Physics2D.gravity.magnitude * (_timeStamp * _timeStamp)) / 2f;
            _points[i].position = pos;
            _timeStamp += _spaceBetweenPoints;
        }
    }
}