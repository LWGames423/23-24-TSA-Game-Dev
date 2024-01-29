using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovementScript : MonoBehaviour
{
    // useful func
    private bool IsMoving
    {
        set
        {
            _isMoving = value;
            _anim.SetBool(Moving, _isMoving);
        }
    }

/*
    private bool IsSprinting
    {
        set
        {
            _isSprinting = value;
            _anim.SetBool(Sprinting, _isSprinting);
        }
    }
    
    
    private bool IsSliding
    {
        set
        {
            _isSliding = value;
            _anim.SetBool(Sliding, _isSliding);
        }
    }
    */



    #region Variables
    [Header("Inputs")]
    private PlayerInput _controls;
    private InputAction _sprint;
    private InputAction _slide;

    [Header("Useful Setup")]
    private Rigidbody2D _rb;
    private Vector2 _input = Vector2.zero;
    private SpriteRenderer _sr;
    private Animator _anim;

    [Header("Stat Controllers")]
    //public StaminaController stam;
    //public float maxStamina = 100f;
    //[FormerlySerializedAs("staminaSubtracter")] public float sprintStaminaSubtracter = 1f;

    [Header("Movement")]
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f; // % of speed deleted from velocity per Time.deltaTime
    bool _isMoving = false;
    public bool canMove = true;

    [Header("Audio")] 
    public AudioSource walk;
    public AudioClip walkSound;
    public float timeBetweenFootsteps = 0.3f;
    private float _timeSinceLastFootstep;

/*
    [Header("Sprint")]
    public float sprintMultiplier = 1.5f;
    private bool _isSprinting = false;
    private bool _canSprint = true;

    [Header("Slide")]
    public float slideMultiplier = 10f;
    public float slideTime = .1f;
    public float slideCooldown = 1f;
    private Vector2 _slideDir;
    private bool _isSliding = false;
    private bool _canSlide = true;
    //private bool _slideStam = false;
    private Vector2 _slideInput;
    */

    [Header("Animator Hashes")]
    private static readonly int Moving = Animator.StringToHash("isMoving");
   // private static readonly int Sprinting = Animator.StringToHash("isRunning");
    //private static readonly int Sliding = Animator.StringToHash("isSliding");
    private static readonly int SwordAttack = Animator.StringToHash("swordAttack");
    #endregion

    #region Audio
    
    
    #endregion

    private void Awake()
    {
        _rb = gameObject.AddComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _rb.angularDrag = 0f;
        _rb.gravityScale = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _controls = GetComponent<PlayerInput>();
        //_sprint = _controls.actions["Sprint"];
        //_slide = _controls.actions["Slide"];

        //stam.maxStam = maxStamina;

    }

    private void FixedUpdate()
    {
        if (canMove && _input != Vector2.zero)
        {
            /*
            if (_isSprinting)
            {
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity + (_input * (moveSpeed * Time.deltaTime)), maxSpeed * sprintMultiplier);
                //stam.LoseStamina(sprintStaminaSubtracter);
            }
            */
            //else
            //{
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity + (_input * (moveSpeed * Time.deltaTime)), maxSpeed);
            if (Time.time - _timeSinceLastFootstep >= timeBetweenFootsteps)
            {
                walk.PlayOneShot(walkSound);
                _timeSinceLastFootstep = Time.time;
            }
            
            
            //}

            if (_input.x > 0)
            {
                _sr.flipX = false;
            }
            else if (_input.x < 0)
            {
                _sr.flipX = true;
            }

            IsMoving = true;
            
        }
        else
        {
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, idleFriction);
            IsMoving = false;
        }

        //checkStam();
        //CheckSprint();
        //Slide();
    }

    void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    void LockMovement()
    {
        canMove = false;
    }

    void UnlockMovement()
    {
        canMove = true;
    }
/*
    private void CheckSprint()
    {
        if (Mathf.Abs(_sprint.ReadValue<float>()) > 0f && _canSprint)
        {
            IsSprinting = true;
        }
        else
        {
            IsSprinting = false;
        }
    }

    private void Slide()
    {
        if (Mathf.Abs(_slide.ReadValue<float>()) > 0f && _canSlide)
        {
            IsSliding = true;
            ResetSlide();
            _slideDir = new Vector2(_input.x, _input.y);
            //stam.LoseStamina(stam.maxStam / 10f);
            StartCoroutine(StopSliding());
        }

        if (_isSliding)
        {
            _rb.velocity = Vector2.ClampMagnitude(_slideDir * slideMultiplier, maxSpeed * slideMultiplier);
        }
    }

    private void ResetSlide()
    {
        _canSlide = false;
        StartCoroutine(SlideCooldown());
    }

    private IEnumerator StopSliding()
    {
        yield return new WaitForSeconds(slideTime);
        IsSliding = false;
    }

    private IEnumerator SlideCooldown()
    {
        yield return new WaitForSeconds(slideCooldown);
        _canSlide = true;
    }

    private void checkStam()
    {
        _slideStam = stam.currentStam - (stam.maxStam / 10f) >= 0;
        _canSprint = stam.currentStam - sprintStaminaSubtracter >= 0;
    }
    */
}
