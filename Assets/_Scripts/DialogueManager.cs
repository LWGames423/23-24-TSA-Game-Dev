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
    public GameObject continueObject;

    public float timePerSection;
    public bool flex;

    private List<string> characters;
    private IEnumerator typeDialogue;
    private IEnumerator nextDialogue;
    private bool finished;

    private void OnEnable()
    {
        currentTextID = 0;
        characters = new List<string>();

        TypeText();
    }

    public void TypeText()
    {
        Clear();
        Debug.Log(currentTextID);
        continueObject.SetActive(false);
        currentText = text[currentTextID];

        for (int i = 0; i < currentText.Length; i++)
        {
            characters.Add(currentText[i].ToString());
        }

        typeDialogue = typeCharacters(characters, timePerSection / characters.Count);
        nextDialogue = nextSection();

        StartCoroutine(typeDialogue);
    }

    IEnumerator typeCharacters(List<string> character, float time)
    {
        for (int i = 0; i < currentText.Length; i++)
        {
            dialogueText.text += character[i];
            yield return new WaitForSeconds(time);
        }

        finished = true;
        character.Clear();
        StartCoroutine(nextDialogue);
    }

    IEnumerator nextSection()
    {
        yield return new WaitUntil(() => finished == true);
        continueObject.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown("space"));
        finished = false;
        continueObject.SetActive(false);
        NextText();
        TypeText();
    }

    public void FastForwardText()
    {
        StopCoroutine(typeDialogue);
        characters.Clear();
        finished = true;

        dialogueText.text = currentText;
        StartCoroutine(nextDialogue);
    }
    
    public void NextText()
    {
        if (currentTextID + 1 < text.Length)
        {
            dialogueText.text = "";
            currentTextID++;
            currentText = text[currentTextID];
        }
        else
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
        }
    }

    public void Clear()
    {
        dialogueText.text = "";
        StopAllCoroutines();
    }
}
