using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed = 5.0f;

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * movespeed, Input.GetAxis("Vertical") * movespeed);
    }
}
