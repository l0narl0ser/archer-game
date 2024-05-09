using UnityEngine;

namespace Game
{
    public class ArrowShoot : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotPoint;

        [SerializeField]
        private GameObject _arrow;


        public void Shoot(Vector2 force)
        {
            GameObject arrowInstantiate = Instantiate(_arrow, _shotPoint.position, _shotPoint.rotation);
            arrowInstantiate.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}