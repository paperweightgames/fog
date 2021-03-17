using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Transform _camera;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;
        private Vector2 _input;
        private bool _jumping;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _moveAction.action.performed += OnMove;
            _jumpAction.action.performed += OnJump;
        }

        private void OnEnable()
        {
            _moveAction.action.Enable();
            _jumpAction.action.Enable();
        }

        private void OnDisable()
        {
            _moveAction.action.Disable();
            _jumpAction.action.Disable();
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            _jumping = obj.ReadValue<float>() > .5;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            _input = obj.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            var velocity = _rb.velocity;
            // Move.
            var moveVelocity = GetMoveVelocity();
            velocity.x = moveVelocity.x;
            velocity.z = moveVelocity.z;
            // Jump.
            if (_jumping && _groundCheck.IsGrounded())
            {
                _jumping = false;
                velocity.y = _jumpHeight;
            }
            _rb.velocity = velocity;
        }

        private Vector3 GetMoveVelocity()
        {
            var right = _camera.right;
            var forward = _camera.forward;
            var cameraRight = new Vector3(right.x, 0, right.z).normalized;
            var cameraForward = new Vector3(forward.x, 0, forward.z).normalized;
            var xVel = _speed * _input.x * cameraRight;
            var yVel = _speed * _input.y * cameraForward;
            return xVel + yVel;
        }
    }
}
