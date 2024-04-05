using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject dialogueCanvas;

    private bool interactable;


    private void Start()
    {
        dialogueCanvas.SetActive(false);

        interactable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Object detected");
        interactCanvas.GetComponent<Animator>().SetBool("Activated", true);
        interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactCanvas.GetComponent<Animator>().SetBool("Activated", false);
        dialogueCanvas.GetComponent<DialogueManager>().Clear();
        dialogueCanvas.SetActive(false);
        interactable = false;
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            dialogueCanvas.SetActive(true);
        }
    }
}
