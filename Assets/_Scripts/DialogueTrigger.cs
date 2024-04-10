using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject dialogueCanvas;

    private bool interactable;
    private PlayerCombat playerCombat;

    public bool givesItem;
    public int itemSelector; //1 for disabler, 2 for debugger, 3 for mender


    private void Start()
    {
        dialogueCanvas.SetActive(false);

        interactable = false;

        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Object detected");
        if (collision.tag == "Player")
        {
            interactCanvas.GetComponent<Animator>().SetBool("Activated", true);
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactCanvas.GetComponent<Animator>().SetBool("Activated", false);
            dialogueCanvas.GetComponent<DialogueManager>().Clear();
            dialogueCanvas.SetActive(false);
            interactable = false;
        }
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            dialogueCanvas.SetActive(true);
            if (givesItem)
            {
                if(itemSelector == 1)
                {
                    playerCombat.hasDisabler = true;
                }
                else if (itemSelector == 2)
                {
                    playerCombat.hasDebugger = true;
                }
                else if (itemSelector == 3)
                {
                    playerCombat.hasMender = true;
                }
            }
        }
    }
}
