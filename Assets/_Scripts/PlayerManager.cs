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
    
    [Header("Checks")]
    public Vector2 checkRadius;
    
    [Header("Timer")] 
    public float coyoteTime;
    
    #endregion
}
