using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    private bool IsMoving
    {
        set
        {
            _isMoving = value;
            _anim.SetBool(Moving, _isMoving);
        }
    }
    
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f; // % of speed deleted from velocity
    private new Rigidbody2D _rb;
    private Vector2 _input = Vector2.zero;
    private SpriteRenderer _sr;
    private Animator _anim;

    bool _isMoving = false;
    public bool canMove = true;
    private static readonly int Moving = Animator.StringToHash("isMoving");
    private static readonly int SwordAttack = Animator.StringToHash("swordAttack");
    
    private void Awake()
    {
        _rb = gameObject.AddComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _rb.angularDrag = 0f;
        _rb.gravityScale = 0f;
    }
    
    private void FixedUpdate()
    {
        if (canMove && _input != Vector2.zero)
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity + (_input * (moveSpeed * Time.deltaTime)), maxSpeed);

            if (_input.x > 0)
            {
                _sr.flipX = false;
            }
            else if(_input.x < 0)
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
    }

    void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    void OnFire()
    {
        _anim.SetTrigger(SwordAttack);
    }

    void LockMovement()
    {
        canMove = false;
    }

    void UnlockMovement()
    {
        canMove = true;
    }
}
