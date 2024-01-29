using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject panel;

    public MovementScript movementScript;
    void OnTriggerEnter2D(Collider2D other)
    {

        if(panel != null && other.CompareTag("Player"))
        {
            bool isActive = panel.activeSelf;

            panel.SetActive(!isActive);

            movementScript.LockMovement();
        }
    }


}
