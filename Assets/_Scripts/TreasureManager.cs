using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    private static TreasureManager instance;
    public int treasureCount = 0;
    public TMP_Text treasureDisplay;
    
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        treasureDisplay.text = "Treasure: " + treasureCount.ToString();
    }

    public void AddTreasure(int count)
    {
        treasureCount += count;
    }
    public void SubtractTreasure(int count)
    {
        treasureCount -= count;
    }

}
