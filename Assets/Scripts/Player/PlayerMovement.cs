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

        [SerializeField] private PlayerFace currentPlayerFace = PlayerFace.DOWN;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsSneaking = Animator.StringToHash("isSneaking");
        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        private static readonly int AnimatorPlayerFace = Animator.StringToHash("playerFace");

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
            float verticalVelocity = verticalMove * Time.fixedDeltaTime * movementSpeed;
            float horizontalVelocity = horizontalMove * Time.fixedDeltaTime * movementSpeed;
            if (isSneaking)
            {
                verticalVelocity *= _sneakSpeedMultiplier;
                horizontalVelocity *= _sneakSpeedMultiplier;
            }

            Vector2 targetVelocity = new Vector2(horizontalVelocity, verticalVelocity);
            // And then smoothing it out and applying it to the character
            _rigidbody2D.velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref _velocity, _movementSmoothing);
        }

        private void updatePlayerFace()
        {
            resetAnimatorValues();
            if (horizontalMove > 0.5F && Math.Abs(horizontalMove) >= Math.Abs(verticalMove))
            {
                currentPlayerFace = PlayerFace.RIGHT;
                isRunning = true;
            }
            else if (horizontalMove < -0.5F && Math.Abs(horizontalMove) >= Math.Abs(verticalMove))
            {
                currentPlayerFace = PlayerFace.LEFT;
                isRunning = true;
            }
            else if (verticalMove > 0.5F && Math.Abs(verticalMove) >= Math.Abs(horizontalMove))
            {
                currentPlayerFace = PlayerFace.UP;
                isRunning = true;
            }
            else if (verticalMove < -0.5F && Math.Abs(verticalMove) >= Math.Abs(horizontalMove))
            {
                currentPlayerFace = PlayerFace.DOWN;
                isRunning = true;
            }
            else
            {
                currentPlayerFace = PlayerFace.DOWN;
                isIdle = true;
            }

            applyAnimatorState();
        }

        private void resetAnimatorValues()
        {
            isRunning = false;
            isIdle = false;
        }

        /// <summary>
        /// Applies the animator state to the animator component
        /// </summary>
        private void applyAnimatorState()
        {
            _animator.SetBool(IsRunning, isRunning);
            _animator.SetBool(IsIdle, isIdle);
            _animator.SetBool(IsSneaking, isSneaking);
            _animator.SetInteger(AnimatorPlayerFace, currentPlayerFace.GetHashCode());
        }

        private enum PlayerFace
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }
    }
}