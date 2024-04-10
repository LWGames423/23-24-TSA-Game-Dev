using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidPlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Transform playerTransform;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerTransform.position = playerTransform.position + new Vector3(horizontal, vertical, 0.0f) * moveSpeed;
    }
}
