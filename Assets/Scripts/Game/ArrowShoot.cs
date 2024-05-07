﻿using System;
using Unity.Mathematics;
using UnityEngine;

namespace Game
{
    public class ArrowShoot : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotPoint;

        [SerializeField]
        private GameObject _arrow;

        [SerializeField]
        private float _force;

        private Vector2 _startPoint;
        private Vector2 _endPoint;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            GameObject arrowInstantiate = Instantiate(_arrow, _shotPoint.position, _shotPoint.rotation);
            arrowInstantiate.GetComponent<Rigidbody2D>().velocity = transform.right * _force;
        }
    }
}