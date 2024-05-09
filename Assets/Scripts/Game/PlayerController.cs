using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerBody;

        [SerializeField]
        private Camera _gameCamera;

        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        [SerializeField]
        private List<AnimationReferenceAsset> _animationReferenceAssets = new List<AnimationReferenceAsset>();

        [SerializeField]
        private string _currentAnimation = "";

        private Vector3 _lastMousePosition;
        private float _timeScale = 1f;

        private const string PLAYER_IDLE = "idle";
        private const string PLAYER_ATTACK_START = "attack_start";
        private const string PLAYER_ATTACK_FINISH = "attack_finish";

        private void Update()
        {
            Vector2 direction = GetInputDirection();
            UpdatePlayerAnimation();
            if (Input.GetMouseButton(0))
            {
                LeanPlayer(direction);
            }
        }

        private void UpdatePlayerAnimation()
        {
            if (_currentAnimation == "" && !Input.GetMouseButton(0))
            {
                SetCharterState(PLAYER_IDLE, true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                SetCharterState(PLAYER_ATTACK_START, false);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetCharterState(PLAYER_ATTACK_FINISH, false);
            }
        }

        private Vector2 GetInputDirection()
        {
            Vector3 currentMousePosition = _gameCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 moveDirection = (currentMousePosition - transform.position).normalized;

            return new Vector2(moveDirection.x, moveDirection.y);
        }

        private void LeanPlayer(Vector2 direction)
        {
            float leanAngleRadians = Vector2.SignedAngle(Vector2.right, direction);
            if (leanAngleRadians > 90 || leanAngleRadians < -90)
            {
                leanAngleRadians = 180 + leanAngleRadians;
            }

            Quaternion quaternion = Quaternion.Euler(0f, 0f, leanAngleRadians);
            _playerBody.rotation = Quaternion.Slerp(quaternion, _playerBody.rotation, Time.deltaTime * 10);
        }

        private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
        {
            if (animation.name.Equals(_currentAnimation))
            {
                return;
            }

            _skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
            _currentAnimation = animation.name;
        }

        private void SetCharterState(string state, bool loop)
        {
            if (state.Equals(PLAYER_ATTACK_FINISH))
            {
                SetAnimation(FindAnimationByName(state), loop, _timeScale);
                Invoke(nameof(SetIdleState), _skeletonAnimation.state.GetCurrent(0).Animation.Duration);
            }
            else
            {
                SetAnimation(FindAnimationByName(state), loop, _timeScale);
            }

            _currentAnimation = state;
        }

        private AnimationReferenceAsset FindAnimationByName(string animationName)
        {
            foreach (var animationReference in _animationReferenceAssets)
            {
                if (animationReference.Animation.Name == animationName)
                {
                    return animationReference;
                }
            }

            return null;
        }

        private void SetIdleState()
        {
            if (!_currentAnimation.Equals(PLAYER_IDLE))
            {
                SetAnimation(FindAnimationByName(PLAYER_IDLE), true, _timeScale);
                _currentAnimation = PLAYER_IDLE;
            }
        }
    }
}