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

        public void Exit()
        {
            UnityEngine.Application.Quit();
        }
    }
}