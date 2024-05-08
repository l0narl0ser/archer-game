using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _arrowRb;
       

       

        private bool hasHit = false;

        private void Update()
        {
            if (hasHit == false)
            {
                TrackMovement();
            }
           
        }

        private void TrackMovement()
        {
            
            float angle = Mathf.Atan2(_arrowRb.velocity.y,_arrowRb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            hasHit = true;
            Destroy(gameObject);
        }

     
      
    }
}