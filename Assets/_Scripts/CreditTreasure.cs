using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditTreasure : MonoBehaviour
{
    public TMP_Text textComponent;
    public TreasureManager tm;

    private void Awake()
    {
        tm = FindObjectOfType<TreasureManager>();
        textComponent.SetText(string.Format("Treasure: {0}", (tm.treasureCount)));
        Destroy(tm.gameObject);
    }
}
