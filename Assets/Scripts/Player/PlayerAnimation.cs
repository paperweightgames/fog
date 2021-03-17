using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Rigidbody _rb;
        private Animator _animator;
        private int _animation = Idle;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var velocity = _rb.velocity;
            if (!_groundCheck.IsGrounded())
                SetAnimation(Jump);
            else if (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z) > .1)
                SetAnimation(Run);
            else
                SetAnimation(Idle);
        }

        private void SetAnimation(int anim)
        {
            if (anim == _animation)
                return;
            _animator.SetBool(_animation, false);
            _animation = anim;
            _animator.SetBool(_animation, true);
        }
    }
}
