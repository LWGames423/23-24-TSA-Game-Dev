using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollingText : MonoBehaviour
{

    public float scrollSpeed = 30f;

    public TMP_Text textComponent;

    private string targetText;
    private int currentCharIndex;
    // Start is called before the first frame update
 public void SetText(string newText)
    {
        targetText = newText;
        currentCharIndex = 0;
        textComponent.text = "";
    }

    public void StartScrolling()
    {
        InvokeRepeating("TypeText", 0f, 1f / scrollSpeed);
    }

    public void StopScrolling()
    {
        CancelInvoke("TypeText");
    }

    void TypeText()
    {
        if (currentCharIndex < targetText.Length)
        {
            textComponent.text += targetText[currentCharIndex];
            currentCharIndex++;
        }
        else
        {
            StopScrolling();
        }
    }

}
