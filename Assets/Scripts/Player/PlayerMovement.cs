using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float horizontalMove;
        private float verticalMove;

        /**
         * Each of these booleans represents the state in the animator and will be used for movement relevant actions.
         * For example calculating the movement speed
         */
        [SerializeField] private bool isRunning;

        [SerializeField] private bool isSneaking;
        [SerializeField] private bool isIdle;
        [SerializeField] private bool isFacingRight;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsSneaking = Animator.StringToHash("isSneaking");
        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        private static readonly int Vertical = Animator.StringToHash("vertical");
        private static readonly int Horizontal = Animator.StringToHash("horizontal");

        private Animator _animator;
        private Rigidbody2D _rigidbody2D;

        // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [FormerlySerializedAs("_sneakSpeed")] [Range(0, 1)] [SerializeField]
        private float _sneakSpeedMultiplier = 0.4F;

        // How much to smooth out the movement
        [Range(0, .3f)] [SerializeField] private float _movementSmoothing = 0.05f;

        // Maximum amount of movement speed
        [SerializeField] private float movementSpeed = 300;

        private Vector2 _velocity = Vector2.zero;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Sneaking"))
            {
                isSneaking = true;
            }
            else if (Input.GetButtonUp("Sneaking"))
            {
                isSneaking = false;
            }
        }

        public void FixedUpdate()
        {
            applyMovementForce();
            updatePlayerFace();
            applyAnimatorState();
        }

        private void applyMovementForce()
        {
            // Move the character by finding the target velocity
            Vector2 velocity = _rigidbody2D.velocity;
            Vector2 targetVelocity = new Vector2(horizontalMove, verticalMove).normalized;
            if (isSneaking)
            {
                targetVelocity *= _sneakSpeedMultiplier;
            }

            targetVelocity *= (Time.fixedDeltaTime * movementSpeed);

            // And then smoothing it out and applying it to the character
            _rigidbody2D.velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref _velocity, _movementSmoothing);
        }

        private void updatePlayerFace()
        {
            if (Math.Abs(horizontalMove) > 0.1F || Math.Abs(verticalMove) > 0.1F)
            {
                isRunning = true;
                isIdle = false;
            }
            else
            {
                isIdle = true;
                isRunning = false;
            }

            // Flip the character if the movement goes into the opposite side the character is currently facing
            if (horizontalMove > 0 && isFacingRight || horizontalMove < 0 && !isFacingRight)
            {
                isFacingRight = !isFacingRight;
                Flip();
            }

            applyAnimatorState();
        }

        /// <summary>
        /// Applies the animator state to the animator component
        /// </summary>
        private void applyAnimatorState()
        {
            _animator.SetBool(IsRunning, isRunning);
            _animator.SetBool(IsIdle, isIdle);
            _animator.SetBool(IsSneaking, isSneaking);
            _animator.SetFloat(Vertical, verticalMove);
            _animator.SetFloat(Horizontal, horizontalMove);
        }

        private void Flip()
        {
            // Multiply the player's x local scale by -1.
            var transform1 = transform;
            Vector3 theScale = transform1.localScale;
            theScale.x *= -1;
            transform1.localScale = theScale;
        }
    }
}