using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winscreenmanager : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);  
    }
}
