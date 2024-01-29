using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public int treasureCount = 0;
    public TMP_Text treasureDisplay;
    
    void Start()
    {
        
    }

    void Update()
    {
        treasureDisplay.text = treasureCount.ToString();
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
