using System;
using Core;
using UnityEngine;

namespace Game
{
    public class ObjectDestroyer : MonoBehaviour
    {
        public event Action<Vector3> OnDestroyed;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<ArrowController>())
            {
                Destroy(gameObject);
                Context.Instance.GetMessageSystem().ObjectDestroyerEvent.ObjectDestroy(gameObject.transform.position);
            }
        }
    }
}