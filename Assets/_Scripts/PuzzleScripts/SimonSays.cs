using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    public Image followRed, followGreen, followBlue, followYellow;
    public Color litRed, litGreen, litBlue, litYellow;
    System.Random rnd = new System.Random();
    public List<int> followPattern = new List<int>();
    public int runNumber;
    public bool canStartInputting = false;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 7; i++)
        {
            followPattern.Add(rnd.Next(1, 5));
        }
    }

    void Start()
    {

    }

    ////Update is called once per frame
    //void Update()
    //{

    //}

    public void runFollowMeLights(int lightNum) //red is 1, green is 2, blue is 3, yellow is 4
    {
        if (lightNum == 1)
        {
            StartCoroutine(delayColorChange(1f, followRed.color, followRed));
            followRed.color = litRed;
        }
        if (lightNum == 2)
        {
            StartCoroutine(delayColorChange(1f, followGreen.color, followGreen));
            followGreen.color = litGreen;
        }
        if (lightNum == 3)
        {
            StartCoroutine(delayColorChange(1f, followBlue.color, followBlue));
            followBlue.color = litBlue;
        }
        if (lightNum == 4)
        {
            StartCoroutine(delayColorChange(1f, followYellow.color, followYellow));
            followYellow.color = litYellow;
        }
        runNumber++;
    }

    IEnumerator delayColorChange(float delay, Color oriColor, Image lightImage)
    {
        yield return new WaitForSeconds(delay);
        lightImage.color = oriColor;
        yield return new WaitForSeconds(delay-0.5f);
        if (runNumber >= (followPattern.Count))
        {
            canStartInputting = true;
        }
        else
        {
            runFollowMeLights(followPattern[runNumber]);
        }
       
    }
}
