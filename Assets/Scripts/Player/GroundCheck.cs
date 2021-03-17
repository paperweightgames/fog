using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
        private readonly List<Collider> _colliders = new List<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            // Skip if already colliding.
            if (_colliders.Contains(other))
                return;
            _colliders.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            // Skip if not colliding.
            if (!_colliders.Contains(other))
                return;
            _colliders.Remove(other);
        }

        public bool IsGrounded()
        {
            return _colliders.Count > 0;
        }
    }
}
