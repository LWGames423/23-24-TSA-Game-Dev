using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaController : MonoBehaviour
{
    
    public GameObject panel;

    public void OpenPanel()
    {
        if(panel != null)
        {
            bool isActive = panel.activeSelf;

            panel.SetActive(!isActive);
        }
    }
   
}
