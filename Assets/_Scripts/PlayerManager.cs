using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour // manage vars. for playermovement
{
    #region Variables

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
