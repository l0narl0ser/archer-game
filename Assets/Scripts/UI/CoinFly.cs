using System;
using Core;
using Game;
using UnityEngine;

namespace UI
{
    public class CoinFly : MonoBehaviour
    {
        [SerializeField]
        private GameObject _coin;

        [SerializeField]
        private Camera _worldCamera;
        private void Awake()
        {
            Context.Instance.GetMessageSystem().ObjectDestroyerEvent.OnDestroyed += OnObjectDestroyed;
        }

        private void OnObjectDestroyed(Vector3 obj)
        {
            SpawnCoin(obj);
        }

        private void SpawnCoin(Vector3 positionSpawn)
        {
          Vector3 newPosition =   _worldCamera.WorldToScreenPoint(positionSpawn);
          Instantiate(_coin, newPosition, Quaternion.identity, transform);

        }
        private void OnDestroy()
        {
            Context.Instance.GetMessageSystem().ObjectDestroyerEvent.OnDestroyed -= OnObjectDestroyed;
        }
    }
}