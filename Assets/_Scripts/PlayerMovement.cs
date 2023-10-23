using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX, dirY;
    public float speed = 10f;

    // erm... what the giggles?
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * speed, dirY * speed);
    }
}
