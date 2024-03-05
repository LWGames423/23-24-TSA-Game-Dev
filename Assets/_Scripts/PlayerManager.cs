using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour // manage vars. for playermovement
{
    #region Variables

    [Header("Player Stats")] 
    public float health = 100f;
    public float stamina = 100f;
    public float attackStr = 5f;
    
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

    [Header("Movement")] 
    public float dashMult;
    public float dashForce;
    public float dashTime;
    
    [Header("Swim")]
    public float swimSpeed;
    public float swimAcceleration;
    public float swimDeceleration;
    public float swimForce;
    
    public float swimGravMultiplier;
    
    [Header("Checks")]
    public Vector2 checkRadius;
    public Vector2 waterCheckRadius;
    
    [Header("Timer")] 
    public float coyoteTime;
    
    
    #endregion


    
}
