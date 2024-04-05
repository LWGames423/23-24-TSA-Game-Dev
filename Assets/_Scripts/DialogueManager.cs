using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] text;
    private string currentText;
    private int currentTextID;

    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

    public float timePerSection;
    public bool flex;

    private List<string> characters;
    private IEnumerator typeDialogue;

    private void OnEnable()
    {
        currentTextID = 0;
        characters = new List<string>();

        TypeText();
    }

    public void TypeText()
    {
        currentText = text[currentTextID];

        for (int i = 0; i < currentText.Length; i++)
        {
            characters.Add(currentText[i].ToString());
        }

        typeDialogue = typeCharacters(characters, timePerSection / characters.Count);
        StartCoroutine(typeDialogue);
    }

    IEnumerator typeCharacters(List<string> character, float time)
    {
        for (int i = 0; i < currentText.Length; i++)
        {
            dialogueText.text += character[i];
            yield return new WaitForSeconds(time);
        }
    }

    public void FastForwardText()
    {
        StopCoroutine(typeDialogue);
        dialogueText.text = currentText;
        currentTextID++;

        if (currentTextID < text.Length)
        {
            currentText = text[currentTextID];
        }
    }
    
    public void NextText()
    {
        if (text.Length - 1 > currentTextID)
        {
            dialogueText.text = "";
            currentTextID++;
            currentText = text[currentTextID];
        }
    }

    public void Clear()
    {
        dialogueText.text = "";
    }
}
