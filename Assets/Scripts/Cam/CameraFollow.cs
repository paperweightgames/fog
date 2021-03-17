using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cam
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _zoomSensitivity;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;
        [SerializeField] private InputActionReference _zoomAction;
        private float _zoom;

        private void Awake()
        {
            _zoomAction.action.performed += OnZoom;
        }

        private void OnEnable()
        {
            _zoomAction.action.Enable();
        }

        private void OnDisable()
        {
            _zoomAction.action.Disable();
        }

        private void OnZoom(InputAction.CallbackContext obj)
        {
            _zoom += _zoomSensitivity * (obj.ReadValue<float>() / 120);
            _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        }

        private void Update()
        {
            var targetPosition = _target.position + GetOffset();
            transform.position = targetPosition;
        }

        private Vector3 GetOffset()
        {
            var t = transform;
            var xOffset = _offset.x * t.right;
            var yOffset = _offset.y * t.up;
            var zOffset = (_zoom + _offset.z) * t.forward;
            return xOffset + yOffset + zOffset;
        }
    }
}
