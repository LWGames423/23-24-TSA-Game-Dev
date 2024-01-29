using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public GameObject panel;

    public MovementScript movementScript;
    public TimeCountdownScript tcs;
    public TreasureManager tm;
    public RoomBehavior rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(true);
            movementScript.LockMovement();
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
