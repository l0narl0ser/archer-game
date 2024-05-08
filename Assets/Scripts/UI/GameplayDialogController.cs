using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Application = Core.Application;

namespace UI
{
    public class GameplayDialogController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerScore;

        private int score = 0;
        
        private void Awake()
        {
            Context.Instance.GetMessageSystem().ObjectEvent.ONCoinReceived += CalculateScore;
        }

        private void CalculateScore()
        {
            score++;
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (_playerScore!=null)
            {
                _playerScore.text = score.ToString();
            }
        }

        private void OnDestroy()
        {
            Context.Instance.GetMessageSystem().ObjectEvent.ONCoinReceived -= CalculateScore;
        }
    }
}