using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCountdownScript : MonoBehaviour
{
    public float currentTime;
    public float maxTime;
    public TMP_Text timerText;
    public Slider timerHealth;

    void Awake()
    {
        timerHealth = this.gameObject.GetComponent<Slider>();
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }

        DisplayTime(currentTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay - (Mathf.FloorToInt(timeToDisplay)))*100;

        if (timeToDisplay <= 30)
        {
            if (Mathf.FloorToInt(milliseconds) == 0)
            {
                timerText.text = string.Format("{0:00}:{1:00}:00", minutes, seconds);
            }
            else
            {
                timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            }
        }
        else
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        timerHealth.value = timeToDisplay / maxTime;
    }
}
