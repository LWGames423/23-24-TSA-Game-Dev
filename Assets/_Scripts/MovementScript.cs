using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f; // % of speed deleted from velocity
    private new Rigidbody2D _rb;
    private Vector2 _input = Vector2.zero;
    private SpriteRenderer _sr;
    private void Awake()
    {
        _rb = gameObject.AddComponent<Rigidbody2D>();
        _sr = gameObject.AddComponent<SpriteRenderer>();
        _rb.angularDrag = 0f;
        _rb.gravityScale = 0f;
    }
    
    private void FixedUpdate()
    {
        if (_input != Vector2.zero)
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
        }
        else
        {
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, idleFriction);
        }
    }

    void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }
}
