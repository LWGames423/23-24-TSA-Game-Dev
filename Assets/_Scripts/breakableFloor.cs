using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableFloor : MonoBehaviour
{
    public GameObject floor;
    private bool _hasEntered = false;
    public float delayTime = .5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_hasEntered)
        {
            _hasEntered = true;
            StartCoroutine(DelayedActionCoroutine());
        }

    }
    
    private IEnumerator DelayedActionCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        floor.SetActive(false);
    }
}
