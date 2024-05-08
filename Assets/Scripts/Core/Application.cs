using System;
using DG.Tweening;
using UnityEngine;

namespace Core
{
    public class Application : MonoBehaviour
    {
        private static Application _instance;

        public static Application Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<Application>();

                return _instance;
            }
        }

        private void Awake()
        {
            DOTween.Init();
        }
    }
}