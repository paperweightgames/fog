using UnityEngine;

namespace Cam
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            var targetPosition = _target.position + GetOffset();
            transform.position = targetPosition;
        }

        private Vector3 GetOffset()
        {
            var t = transform;
            var xOffset = t.right * _offset.x;
            var yOffset = t.up * _offset.y;
            var zOffset = t.forward * _offset.z;
            return xOffset + yOffset + zOffset;
        }
    }
}
