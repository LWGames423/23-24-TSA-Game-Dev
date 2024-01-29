using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContinuePrice : MonoBehaviour
{
    public TMP_Text textComponent;
    public TreasureManager tm;

    private void Awake()
    {
        tm = FindObjectOfType<TreasureManager>();
        textComponent.SetText(string.Format("Continue {0}", (tm.treasureCount - Mathf.FloorToInt(tm.treasureCount / 2))));
    }
}
