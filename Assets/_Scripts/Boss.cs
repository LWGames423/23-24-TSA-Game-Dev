using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour
{
    public GameObject panel;

    public MovementScript movementScript;


    private ScrollingText scrollingText;

    void Start()
    {
        scrollingText = panel.GetComponentInChildren<ScrollingText>();
    }

   
    

    public TimeCountdownScript tcs;
    public TreasureManager tm;
    public RoomBehavior rb;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(true);
            movementScript.LockMovement();

            scrollingText.SetText("you have entered my dungeon. you may continue, for a small fee, or you may complete my puzzle for a grand reward! but beware, wasting my time comes at a tremendous cost.");
            scrollingText.StartScrolling();
            
            
        }
    }

    private void Awake()
    {
        tcs = FindAnyObjectByType<TimeCountdownScript>();
        tm = FindAnyObjectByType<TreasureManager>();

        movementScript = FindAnyObjectByType<MovementScript>();
    }

    public void Continue()
    {
        tm.treasureCount = Mathf.FloorToInt(tm.treasureCount / 2);
        tcs.initialScore = tm.treasureCount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Puzzle()
    {
        rb.GenerateBossPuzzle();
        panel.SetActive(false);
        movementScript.UnlockMovement();
        this.gameObject.SetActive(false);
    }
}
