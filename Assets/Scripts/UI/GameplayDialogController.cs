using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayDialogController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playerScore;

        private int _score = 0;

        private void Awake()
        {
            Context.Instance.GetMessageSystem().ObjectEvent.OnCoinReceived += CalculateScore;
        }

        private void CalculateScore()
        {
            _score++;
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (_playerScore != null)
            {
                _playerScore.text = _score.ToString();
            }
        }

        private void OnDestroy()
        {
            Context.Instance.GetMessageSystem().ObjectEvent.OnCoinReceived -= CalculateScore;
        }
    }
}