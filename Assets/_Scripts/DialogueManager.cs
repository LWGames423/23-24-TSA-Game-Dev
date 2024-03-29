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

    private void Awake()
    {
        currentTextID = 0;
        currentText = text[currentTextID];
        List<string> characters = new List<string>();

        for (int i = 0; i < currentText.Length; i++)
        {
            characters.Add(currentText[i].ToString());
            Debug.Log(characters[i]);
        }
    }

    IEnumerator typeCharacter(string character, float time)
    {
        dialogueText.text += character;
        yield return new WaitForSeconds(time);
    }

    public void FastForwardText()
    {
        dialogueText.text = currentText;
        currentTextID++;
        currentText = text[currentTextID];
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
}
