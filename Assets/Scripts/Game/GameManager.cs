using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Camera _worldCamera;

        [SerializeField]
        private ArrowShoot _arrowShoot;

        [SerializeField]
        private Transform _arrow;

        [SerializeField]
        private TrajectoryController _trajectory;

        [SerializeField]
        private float _pushForce = 4f;
        
        private Vector3 _endPoint;
        private Vector2 _direction;
        private Vector2 _force;
        private float _distance;
        private bool _isDragging = false;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                OnDragEnd();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                OnDragStart();
            }

            if (_isDragging)
            {
                OnDrag();
            }
        }

        private void OnDragStart()
        {
            _trajectory.ShowTrajectory();
        }

        private void OnDrag()
        {
            _endPoint = _worldCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 currentMousePosition = _worldCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 moveDirection = (currentMousePosition - _arrowShoot.transform.position).normalized;
            _direction = moveDirection * -1;
            _distance = Vector3.Distance(_endPoint, _arrowShoot.transform.position);

            float leanAngleRadians = Vector2.SignedAngle(Vector2.right, moveDirection);
            if (leanAngleRadians < 90 && leanAngleRadians > -90)
            {
                _direction *= -1;
            }

            _force = _direction * _distance * _pushForce;

            _trajectory.UpdateDots(_arrow.position, _force);
        }

        private void OnDragEnd()
        {
            _arrowShoot.Shoot(_force);
            _trajectory.HideTrajectory();
        }
    }
}