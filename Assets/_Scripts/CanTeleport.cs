using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTeleport : MonoBehaviour
{
    public bool teleportCooldown;

    private void Start()
    {
        teleportCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            teleportCooldown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            teleportCooldown = false;
        }
    }
}
