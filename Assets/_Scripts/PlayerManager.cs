using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour // manage vars. for playermovement
{
    #region Variables

    public PlayerMovement pm;
    
    [Header("Player Stats")] 
    public float maxHealth = 100f;
    public float currentHealth;
    public float stamina = 100f;
    public float attackStr = 5f;

    public bool isInvuln;
    
    [Header("Movement")] 
    public bool canMove = true;

    public bool isStarting;

    public bool isEnding;
    
    public float moveSpeed;
    
    public float acceleration;
    public float deceleration;
    public float frictionAmount;
    
    public float velPower;

    [Header("Jump")]
    public float jumpForce;

    public float fallGravityMultiplier;

    public float jumpCutMultiplier;

    public int jumpCount = 1;

    [Header("Dash")] 
    public float dashMult;
    public float dashForce;
    public float dashTime;
    public float dashCooldown;
    
    [Header("Swim")]
    public float swimSpeed;
    public float swimAcceleration;
    public float swimDeceleration;
    public float swimForce;
    
    public float swimGravMultiplier;

    public float maxWaterVel;
    [Header("Checks")]
    public Vector2 checkRadius;
    public Vector2 waterCheckRadius;
    
    [Header("Timer")] 
    public float coyoteTime;
    
    [Header("Regen")]
    public float regenRate = 5f; 
    private float _timeElapsed; 
    public float regenDelay = 2f;

    [Header("Ladder")]
    public float climbSpeed = 10.0f;
    
    #endregion

    private void Start()
    {
        currentHealth = maxHealth;
        _timeElapsed = Time.time; 
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if (Time.time - _timeElapsed > regenDelay)
        {
            RegenHealth();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }

        if (currentHealth <= 0)
        {
            pm.Respawn();
        }
    }
    
    public void TakeDamage(float damage)
    {
        if (!isInvuln)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
        
            _timeElapsed = Time.time;
        }
    }

    void RegenHealth()
    {
        currentHealth += Time.deltaTime * regenRate;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }
}
