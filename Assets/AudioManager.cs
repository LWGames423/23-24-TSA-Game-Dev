using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource source;
    public AudioClip buttonSound;
    public AudioClip backgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            source.PlayOneShot(buttonSound);
            Debug.Log("Click");
        }
    }
}
