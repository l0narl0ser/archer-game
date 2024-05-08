using System;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

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

       

        private void Update()
        {
            Vector2 direction = GetInputDirection();
            if (_currentAnimation == "" && !Input.GetMouseButton(0))
            {
                SetCharterState("idle");
            }

            if (Input.GetMouseButtonDown(0))
            {
                SetCharterState("attack_start");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetCharterState("attack_finish");
            }

            if (Input.GetMouseButton(0))
            {
                LeanPlayer(direction);
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
            leanAngleRadians = Mathf.Clamp(leanAngleRadians, -90, 90f);
            Debug.LogWarning($"{leanAngleRadians}");
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

        private void SetCharterState(string state)
        {
            if (state.Equals("attack_start"))
            {
                SetAnimation(FindAnimationByName("attack_start"), false, 1f);
            }
            else if (state.Equals("attack_finish"))
            {
                SetAnimation(FindAnimationByName("attack_finish"), false, 1f);
                Invoke("SetIdleState", _skeletonAnimation.state.GetCurrent(0).Animation.Duration);
            }
            else
            {
                SetAnimation(FindAnimationByName("idle"), true, 1f);
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
            if (!_currentAnimation.Equals("idle"))
            {
                SetAnimation(FindAnimationByName("idle"), true, 1f);
                _currentAnimation = "idle";
            }
        }
    }
}