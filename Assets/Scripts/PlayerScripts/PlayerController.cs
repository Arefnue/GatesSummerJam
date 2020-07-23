using System;
using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        // Config
        [Header("References")]
        public Rigidbody2D myRigidBody;

        public Camera mainCamera;

        [Header("Jump")]
        [SerializeField] int maxJumpCombo = 2;
        [SerializeField] float jumpSpeed = 10;

        [Header("Movement")]
        [SerializeField] float moveSpeed = 10;
    
        [Header("Ground")] 
        public LayerMask groundLayer;
        public Transform groundCheckPosition;
        public float groundCheckRadius;

        [Header("Knockback")] 
        public float knockbackSpeed;
        public float knockbackLength;
        public float knockbackCount;
        public bool knockFromRight;

        [Header("Audio")] 
        public AudioClip jumpOneClip;
        public AudioClip jumpTwoClip;
    
        // States
        private int _curJumpCombo = 0;
        private float _horizontal = 0f;
        private float _jumpDirection;
        private bool _isGrounded;
        private bool _isFacingRight;

        private Animator _animator;
        private static readonly int State = Animator.StringToHash("State");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (GameManager.Manager.gameOver == true)
            {
                return;
            }
            
            if (_isGrounded)
            {
                _curJumpCombo = 0;
            }

            
        
            Jump();
            LookMouse();

            UpdateAnimationState();

        }

        private void FixedUpdate()
        {
            if (GameManager.Manager.gameOver == true)
            {
                return;
            }
            
            IsTouchingGround();
            Move();
        }

        private void UpdateAnimationState()
        {
            if (!_isGrounded)
            {
                _animator.SetInteger(State, 2);
            }
            else if( Mathf.Abs(_horizontal) > Mathf.Epsilon )
            {
                _animator.SetInteger(State, 1);
            }
            else{
                _animator.SetInteger(State, 0);
            }
        }

        private void LookMouse()
        {
            var delta = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (delta.x < 0 && !_isFacingRight)
            {
                Flip();
            }
            else if (delta.x >= 0 && _isFacingRight)
            {
                Flip();
            }
        }

    
        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _curJumpCombo < maxJumpCombo)
            {
                AudioManager.Manager.PlaySfx(jumpOneClip);
                myRigidBody.velocity = Vector2.up * jumpSpeed;
                _curJumpCombo++;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _curJumpCombo == maxJumpCombo)
            {
                AudioManager.Manager.PlaySfx(jumpTwoClip);
                myRigidBody.velocity = Vector2.up * jumpSpeed;
                
            }
        
        }

        private void IsTouchingGround()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);
            if (_isGrounded)
            {
                _curJumpCombo = 0;
            }
        }
        
        private void Move()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");

            if (knockbackCount <= 0)
            {
                myRigidBody.velocity = new Vector2(_horizontal*moveSpeed,myRigidBody.velocity.y);
            }
            else
            {
                if (knockFromRight)
                {
                    myRigidBody.velocity = new Vector2(-knockbackSpeed,knockbackSpeed);
                }

                if (!knockFromRight)
                {
                    myRigidBody.velocity = new Vector2(knockbackSpeed,knockbackSpeed);
                }

                knockbackCount -= Time.deltaTime;
            }

        }
    
        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            var transform1 = transform;
            var scale = transform1.localScale;
            scale.x *= -1;
            transform1.localScale = scale;
        }
    
    }
}
