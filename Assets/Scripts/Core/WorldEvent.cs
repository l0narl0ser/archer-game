using System;
using UnityEngine;

namespace Core
{
    public class WorldEvent
    {
        public event Action<Vector3> OnDestroyed;

        public void ObjectDestroy(Vector3 position)
        {
            OnDestroyed?.Invoke(position);
        }
    }
}