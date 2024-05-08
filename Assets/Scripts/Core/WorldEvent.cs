using System;
using UnityEngine;

namespace Core
{
    public class WorldEvent
    {
        public event Action<Vector3> OnDestroyed;
        public event Action OnCoinReceived ;
        
        public void ObjectDestroy(Vector3 position)
        {
            OnDestroyed?.Invoke(position);
        }

        public void ReceiveCoin()
        {
            OnCoinReceived?.Invoke();
        }
    }
}