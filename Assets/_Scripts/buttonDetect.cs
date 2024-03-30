using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonDetect : MonoBehaviour
{
    public buttonManager bm;
    
    public bool isBoxActivated = false;
    public bool hold = true;
    private bool _activated = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isBoxActivated)
        {
            if (other.CompareTag("Player") && !_activated)
            {
                bm.Toggle();
                _activated = true;
            }
        } else if (isBoxActivated)
        {
            if (other.CompareTag("Box") && !_activated)
            {
                bm.Toggle();
                _activated = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isBoxActivated)
        {
            if (other.CompareTag("Player") && !hold)
            { 
                bm.Toggle();
                _activated = false;
            }
        } else if (isBoxActivated)
        {
            if (other.CompareTag("Box") && !hold)
            {
                
                bm.Toggle();
                _activated = false;
                
            }
        }
        
    }
}
