using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private Rigidbody _rb;

        private void Update()
        {
            var velocity = _rb.velocity;
            // Skip if no velocity.
            if (velocity.magnitude < .1)
                return;
            var targetForward = new Vector3(velocity.x, 0, velocity.z).normalized;
            transform.forward = Vector3.Lerp(transform.forward, targetForward, _speed);
        }
    }
}
