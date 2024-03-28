using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] text;

    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

    public float timePerSection;
    public bool flex;


}
