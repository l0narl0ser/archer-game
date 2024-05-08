using UnityEngine;

namespace Game
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _arrowRb;

        private bool _hasHit = false;

        private void Update()
        {
            if (_hasHit == false)
            {
                TrackMovement();
            }
        }

        private void TrackMovement()
        {
            float angle = Mathf.Atan2(_arrowRb.velocity.y, _arrowRb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _hasHit = true;
            Destroy(gameObject);
        }
    }
}