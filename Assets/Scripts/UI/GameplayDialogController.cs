using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Application = Core.Application;

namespace UI
{
    public class GameplayDialogController : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        [SerializeField] private TextMeshProUGUI _playerScore;

        private void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
            
        }

        private void OnExitButtonClick()
        {
            Application.Instance.Exit();
            Debug.Log("Exit");
        }
    }
}