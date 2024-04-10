using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  


public class mainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject menu;
    private bool _tutorialActive = false;
    
    
    public void PlayGame() {  
        SceneManager.LoadScene("LevelOne");  
    }  
    
    public void PlayTutorial()
    {
        _tutorialActive = true;
    }

    public void QuitGame()
    {
        Debug.Log("quit game teehee");
        Application.Quit();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _tutorialActive = false;
        tutorialPanel.SetActive(_tutorialActive);
        menu.SetActive(!_tutorialActive);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();

        }
        
        tutorialPanel.SetActive(_tutorialActive);
        menu.SetActive(!_tutorialActive);

    }

    public void Back()
    {
        _tutorialActive = false;
    }
}
