using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cam
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _maxPitch;
        [SerializeField] private InputActionReference _lookAction;
        private Vector2 _input;
        private Vector3 _rotation;

        private void Awake()
        {
            _rotation = transform.eulerAngles;
            _lookAction.action.performed += OnLook;
        }

        private void OnEnable()
        {
            _lookAction.action.Enable();
        }

        private void OnDisable()
        {
            _lookAction.action.Disable();
        }

        private void OnLook(InputAction.CallbackContext obj)
        {
            _input = obj.ReadValue<Vector2>();
        }

        private void Update()
        {
            _rotation += new Vector3(-_input.y, _input.x, 0) * _sensitivity;
            _rotation.x = Mathf.Clamp(_rotation.x, -_maxPitch, _maxPitch);
            transform.rotation = Quaternion.Euler(_rotation);
        }
    }
}
