using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Vector2 movementSpeed = new Vector2(7f, 7f);
    private new Rigidbody2D _rigidbody2D;
    private Vector2 _input = new Vector2(0f, 0f);
    private void Awake()
    {
        _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();

        _rigidbody2D.angularDrag = 0f;
        _rigidbody2D.gravityScale = 0f;
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _input * (movementSpeed * Time.fixedDeltaTime));
    }
}
