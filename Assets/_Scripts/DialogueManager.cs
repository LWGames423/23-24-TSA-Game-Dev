using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool isAFadeDialogue;

    public AudioSource speakingNoise;

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
        speakingNoise.Play();

        for (int i = 0; i < currentText.Length; i++)
        {
            dialogueText.text += character[i];
            yield return new WaitForSeconds(time);
        }

        speakingNoise.Stop();
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
        speakingNoise.Stop();
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
            if (isAFadeDialogue)
            {
                Animator[] anims = FindObjectsOfType<Animator>();
                foreach(Animator anim in anims)
                {
                    if (anim.transform.tag == "FadeCanvas")
                    {
                        anim.SetTrigger("FadeOut");
                    }
                }
            }
        }
    }

    public void Clear()
    {
        dialogueText.text = "";
        StopAllCoroutines();
    }

}
