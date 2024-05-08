using System;
using Core;
using UnityEngine;

namespace Game
{
    public class ObjectDestroyer : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<ArrowController>())
            {
                Destroy(gameObject);
                Context.Instance.GetMessageSystem().ObjectEvent.ObjectDestroy(gameObject.transform.position);
            }
        }
    }
}