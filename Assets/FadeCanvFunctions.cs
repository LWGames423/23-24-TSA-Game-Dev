using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCanvFunctions : MonoBehaviour
{
    public bool isLevel;
    public Animator fade;

    public void transitionScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLevel)
        {
            if(other.gameObject.tag == "Player")
            {
                fade.SetTrigger("FadeOut");
            }
        }
    }
}
