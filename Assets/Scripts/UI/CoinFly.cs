using Core;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class CoinFly : MonoBehaviour
    {
        [SerializeField]
        private GameObject _coinPrefab;

        [SerializeField]
        private RectTransform _targetUIElement;

        [SerializeField]
        private Camera _worldCamera;

        [SerializeField]
        private GameObject _targetPosition;

        private void Awake()
        {
            Context.Instance.GetMessageSystem().ObjectEvent.OnDestroyed += OnObjectDestroyed;
        }

        private void OnObjectDestroyed(Vector3 obj)
        {
            GameObject spawnCoin = SpawnCoin(obj);
            AnimateCoin(spawnCoin);
        }

        private GameObject SpawnCoin(Vector3 positionSpawn)
        {
            Vector3 screenPosition = _worldCamera.WorldToScreenPoint(positionSpawn);
            GameObject spawnedCoin = Instantiate(_coinPrefab, screenPosition, Quaternion.identity, transform);
            return spawnedCoin;
        }

        private void AnimateCoin(GameObject spawnedCoin)
        {
            RectTransform rectSpawnedCoin = spawnedCoin.GetComponent<RectTransform>();
            Vector3 targetPosition = _targetPosition.transform.position;
            Vector3 targetScale = _targetUIElement.rect.size;
            spawnedCoin.transform.DOMove(targetPosition, 1.0f);
            rectSpawnedCoin.DOSizeDelta(targetScale, 1.0f)
                .OnComplete(() =>
                {
                    Destroy(spawnedCoin);
                    Context.Instance.GetMessageSystem().ObjectEvent.ReceiveCoin();
                });
        }

        private void OnDestroy()
        {
            Context.Instance.GetMessageSystem().ObjectEvent.OnDestroyed -= OnObjectDestroyed;
        }
    }
}