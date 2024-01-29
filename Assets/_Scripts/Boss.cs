using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    public GameObject panel;

    public MovementScript movementScript;

    private ScrollingText scrollingText;

    void Start()
    {
        scrollingText = panel.GetComponentInChildren<ScrollingText>();
    }

   
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if(panel != null && other.CompareTag("Player"))
        {
            bool isActive = panel.activeSelf;

            panel.SetActive(!isActive);

            movementScript.LockMovement();

            scrollingText.SetText("you have entered my dungeon. you may continue, for a small fee, or you may complete my puzzle for a grand reward! but beware, wasting my time comes at a tremendous cost.");
            scrollingText.StartScrolling();
            
            
        }
    }


}
