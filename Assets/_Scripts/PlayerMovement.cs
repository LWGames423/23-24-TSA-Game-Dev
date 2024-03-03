using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // public Animator animator;
    public float currentSpeed;
    public bool isWalking = false;
    [FormerlySerializedAs("YVel")] public float yVel;

    public PlayerManager pm;

    public InputAction playerMovement;

    public GameObject playerSpawn;
    // public GameObject playerEnd; // add an prefab where the level will end & start ending cutscene

    public LayerMask groundLayer;
    public Transform groundCheck;
    
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private bool _facingRight = true;
    public bool _isGrounded;
    

    public bool _canJump;
    private bool _isJumping;

    private readonly float _gravityScale = 1f;
    private float _ctc; // coyote time counter

    private void OnEnable()
    {
        playerMovement.Enable();
        // playerDash.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
        // playerDash.Disable();
    }

    private void Awake()
    {
        transform.position = playerSpawn.transform.position;
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        
        _facingRight = true;
        _canJump = true;
    }
    
    private void Update()
    {
        #region InputChecks
        
        // _moveInput.x = Input.GetAxisRaw(xInput);
        // _moveInput.y = Input.GetAxisRaw(yInput);
        // _dashInput.x = Input.GetAxisRaw(dashInput);
        // _dashInput.y = 0f;

        if (pm.canMove)
        {
            _moveInput = playerMovement.ReadValue<Vector2>();
            // _dashInput.x = playerDash.ReadValue<float>();
        }
        else
        {
            _rb.velocity = Vector2.zero;
            _moveInput = Vector2.zero;
        }

        if (pm.isStarting)
        {
            transform.position = playerSpawn.transform.position;
        }

        /*if (pm.isEnding)
        {
            transform.position = playerEnd.transform.position;
        }*/
        
        
        

        #endregion

        #region Jump

        if (_moveInput.y > 0  && _canJump && _ctc > 0 && _isJumping==false)
        {
            _ctc = 0;
            _rb.velocity = new Vector2(_rb.velocity.x, pm.jumpForce);
            _canJump = false;
            _isJumping = true;
            Jump();
        }

        if (_moveInput.y < 0.01 && _isJumping)
        {
            JumpUp();
        } 

        #endregion

        #region FlipPlayer

        if (_moveInput.x > 0 && !_facingRight)
        {
            Flip();
            _facingRight = true;
        }
        else if(_moveInput.x < 0 && _facingRight)
        {
            Flip();
            _facingRight = false;
        }

        #endregion

        #region Timer

        _ctc -= Time.deltaTime;

        #endregion

    }

    private void FixedUpdate()
    {
        #region pm.groundCheck

        _isGrounded = Physics2D.OverlapBox(groundCheck.position, pm.checkRadius, 0, groundLayer);
        if (_isGrounded && _moveInput.y < 0.01)
        {
            _canJump = true;
            _isJumping = false;
            
            _ctc = pm.coyoteTime;
        }

        #endregion

        #region Jump Gravity

        if(_rb.velocity.y < 0)
        {
            _rb.gravityScale = _gravityScale * pm.fallGravityMultiplier;
            _isJumping = false;
        }
        else
        {
            _rb.gravityScale = _gravityScale;
        }
        

        #endregion
        
        #region Dash

        // if (_dashInput.x > 0 && _canDash)
        // {
        //     _canDash = false;
        //     _isDashing = true;
        //     _dashDir = new Vector2(_moveInput.x * pm.horMult, _moveInput.y * pm.vertMult);
        //     if (_dashDir == Vector2.zero)
        //     {
        //         _dashDir = new Vector2(transform.localScale.x, 0);
        //     }   
        //     
        //     StartCoroutine(StopDashing());
        // }
        //
        // if(_isDashing)
        // {
        //     _rb.velocity = _dashDir * pm.dashForce;
        // }
        
        #endregion
        
        #region Run
        
        float targetSpeed = _moveInput.x * pm.moveSpeed;
        float speedDif = targetSpeed - _rb.velocity.x;
        
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? pm.acceleration : pm.deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, pm.velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);

        #endregion
        
        #region Friction

        if (_isGrounded && Mathf.Abs(_moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(pm.frictionAmount));

            amount *= Mathf.Sign(_rb.velocity.x);
            
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        
        #endregion

        // #region AnimationComponents
        //
        // currentSpeed = _rb.velocity.x;
        // isWalking = Mathf.Abs(_moveInput.x) > 0.01f;
        // yVel = _rb.velocity.y;
        //
        // animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        // animator.SetFloat("YVel", yVel);
        // animator.SetBool("isWalking", isWalking);
        // animator.SetBool("isGrounded", _isGrounded);
        //
        //
        // #endregion
    }

    #region Flip
    
    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
    #endregion

    #region Jump Script

    void Jump()
    {
        _rb.AddForce(Vector2.up * pm.jumpForce, ForceMode2D.Impulse);
    }

    void JumpUp()
    {
        if (_rb.velocity.y > 0 && _isJumping)
        {
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - pm.jumpCutMultiplier), ForceMode2D.Impulse);
            // _rb.AddForce(Vector2.down * _rb.velocity.y * -0.8f, ForceMode2D.Impulse);
        }
    }
    
    #endregion

    #region Coroutines


    #endregion

    #region Death

    public void Death()
    {
        _rb.velocity = Vector2.zero;
        transform.position = playerSpawn.transform.position;
    }

    #endregion
    
}