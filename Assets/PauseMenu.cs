using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool _isActive = false;

    public GameObject pauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isActive)
        {
            _isActive = true;
        } else if(Input.GetKeyDown(KeyCode.Escape) && _isActive)
        {
            _isActive = false;
        }

        if (_isActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        pauseMenu.SetActive(_isActive);
    }

    public void Resume()
    {
        _isActive = false;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);  
    }
    
}
