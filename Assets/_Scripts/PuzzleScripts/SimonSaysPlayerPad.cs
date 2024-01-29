using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonSaysPlayerPad : MonoBehaviour
{
    public SimonSays ss;
    public DetectMouseHover dMH;
    public Image checkBar;
    public Color successColor, failColor;
    public List<int> ssCorrectList = new List<int>();
    public int losses = 0;
    public GameObject loss1, loss2, loss3;
    public int runNum = 0;
    public TimeCountdownScript tcs;
    public TreasureManager tm;
    public RoomGeneration roomGeneration;

    private void Awake()
    {
        tm = FindAnyObjectByType<TreasureManager>();
        tcs = FindAnyObjectByType<TimeCountdownScript>();
        roomGeneration = FindAnyObjectByType<RoomGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ss.canStartInputting == true)
        {
            ssCorrectList = ss.followPattern;
        }
        if(losses >= 1)
        {
            loss1.GetComponent<Image>().color = failColor;
        }
        if (losses >= 2)
        {
            loss2.GetComponent<Image>().color = failColor;
        }
        if (losses >= 3)
        {
            loss3.GetComponent<Image>().color = failColor;
            //add a fail function here
        }
    }

    public void redButton()
    {
        if(ss.canStartInputting == true)
        {
            if(ssCorrectList[runNum] == 1)
            {
                if(runNum >= 6)
                {
                    

                    if (dMH.isBossPuzzle == true)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        tcs.currentTime += 30;
                        tm.AddTreasure(10);
                        roomGeneration.keyRooms--;
                    }

                    checkBar.color = successColor;
                    dMH.hasCompletedSimon = true;
                }
                runNum++;
            }
            else
            {
                if (losses >= 2)
                {
                    Debug.Log("welp you lost");
                    if (dMH.isBossPuzzle == true)
                    {
                        tcs.Restart();
                    }
                    dMH.hasCompletedSimon = true;
                }
                else
                {
                    losses++;
                    checkBar.color = failColor;
                    runNum = 0;
                    ss.runNumber = 0;
                    ss.canStartInputting = false;
                    ss.runFollowMeLights(ss.followPattern[ss.runNumber]);
                }
                
            }

        }
    }

    public void greenButton()
    {
        if (ss.canStartInputting == true)
        {
            if (ssCorrectList[runNum] == 2)
            {
                if (runNum >= 6)
                {
                    if (dMH.isBossPuzzle == true)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        tcs.currentTime += 30;
                        tm.AddTreasure(10);
                        roomGeneration.keyRooms--;
                    }

                    checkBar.color = successColor;
                    dMH.hasCompletedSimon = true;
                }
                runNum++;
            }
            else
            {
                if (losses >= 2)
                {
                    Debug.Log("welp you lost");
                    if (dMH.isBossPuzzle == true)
                    {
                        tcs.Restart();
                    }
                    dMH.hasCompletedSimon = true;
                }
                else
                {
                    losses++;
                    checkBar.color = failColor;
                    runNum = 0;
                    ss.runNumber = 0;
                    ss.canStartInputting = false;
                    ss.runFollowMeLights(ss.followPattern[ss.runNumber]);
                }
            }
            
        }
    }

    public void blueButton()
    {
        if (ss.canStartInputting == true)
        {
            if (ssCorrectList[runNum] == 3)
            {
                if (runNum >= 6)
                {
                    if (dMH.isBossPuzzle == true)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        tcs.currentTime += 30;
                        tm.AddTreasure(10);
                        roomGeneration.keyRooms--;
                    }

                    checkBar.color = successColor;
                    dMH.hasCompletedSimon = true;
                }
                runNum++;
            }
            else
            {
                if (losses >= 2)
                {
                    Debug.Log("welp you lost");
                    if (dMH.isBossPuzzle == true)
                    {
                        tcs.Restart();
                    }
                    dMH.hasCompletedSimon = true;
                }
                else
                {
                    losses++;
                    checkBar.color = failColor;
                    runNum = 0;
                    ss.runNumber = 0;
                    ss.canStartInputting = false;
                    ss.runFollowMeLights(ss.followPattern[ss.runNumber]);
                }
            }

        }
    }

    public void yellowButton()
    {
        if (ss.canStartInputting == true)
        {
            if (ssCorrectList[runNum] == 4)
            {
                if (runNum >= 6)
                {
                    if (dMH.isBossPuzzle == true)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        tcs.currentTime += 30;
                        tm.AddTreasure(10);
                        roomGeneration.keyRooms--;
                    }

                    checkBar.color = successColor;
                    dMH.hasCompletedSimon = true;
                }
                runNum++;
            }
            else
            {
                if (losses >= 2)
                {
                    Debug.Log("welp you lost");
                    if (dMH.isBossPuzzle == true)
                    {
                        tcs.Restart();
                    }
                    dMH.hasCompletedSimon = true;
                }
                else
                {
                    losses++;
                    checkBar.color = failColor;
                    runNum = 0;
                    ss.runNumber = 0;
                    ss.canStartInputting = false;
                    ss.runFollowMeLights(ss.followPattern[ss.runNumber]);
                }
            }

        }
    }
}
