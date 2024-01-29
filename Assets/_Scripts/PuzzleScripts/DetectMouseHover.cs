using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DetectMouseHover : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite hoverSprite, regSprite;
    public bool isOnSprite, isInstructions, isChest, isSimonDesk;
    public GameObject PuzzleUI, ChestSpecificUI, Chest1, Chest2, SimonUI;
    public String ChestText;
    public int correctChest;
    public int thisChestNum;
    public int problemNum;
    public ChestProblemNumGenerator numGenerator;
    public List<string> Chest1Strings = new List<string>() { "The treasure is not in this chest", "All of the chests tell the truth.", "This chest does not have treasure. At least one other chest tells the truth.", "This chest contains the treasure, and I tell the truth.", "The treasure is not in any of these chests.", "Chest 2 tells the truth." };
    public List<string> Chest2Strings = new List<string>() { "One chest tells the truth.", "At least one chest lies, and the treasure is not in Chest 3.", "This chest does not contain the treasure, however we all tell the truth.", "The other chests tell the truth, and the treasure is in Chest 3.", "The treasure is in this chest.", "This chest contains the treasure." };
    public List<string> Chest3Strings = new List<string>() { "The other chests tell the truth, and the treasure is not in Chest 1.", "Chest 1 tells the truth and the treasure is in his chest.", "One of the other chests lies.", "Both of the other chests lie, the treasure is in this chest.", "The treasure is not in this chest, and the others both lie.", "Both of the other chests tell the truth, and Chest 1 has the treasure." };
    public List<int> correctAnswers = new List<int>() { 1, 2, 2, 1, 1, 3 };
    public bool hasCompleted = false, hasCompletedSimon = false;
    public RoomGeneration roomGeneration;
    public TreasureManager treasureManager;

    private void Awake()
    {
        problemNum = numGenerator.problemNum;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        correctChest = correctAnswers[problemNum];

        roomGeneration = FindAnyObjectByType<RoomGeneration>();
        treasureManager = FindAnyObjectByType<TreasureManager>();
    }

    void OnMouseOver()
    {
        renderer.sprite = hoverSprite;
        isOnSprite = true;
    }

    void OnMouseExit()
    {
        renderer.sprite = regSprite;
        isOnSprite = false;
    }

    void Update()
    {
        if (hasCompletedSimon)
        {
            SimonUI.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnSprite)
            {
                if (isInstructions)
                {
                    if (!hasCompleted)
                    {
                        PuzzleUI.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("you've already done this puzzle");
                    }
                }
                if (isSimonDesk)
                {
                    if (!hasCompletedSimon)
                    {
                        SimonUI.SetActive(true);
                        SimonUI.GetComponentInParent<SimonSays>().runFollowMeLights(SimonUI.gameObject.GetComponentInParent<SimonSays>().followPattern[SimonUI.gameObject.GetComponentInParent<SimonSays>().runNumber]);
                    }
                    else
                    {
                        Debug.Log("you've already done this puzzle");
                    }
                }
                if (isChest)
                {
                    if (thisChestNum == 1)
                    {
                        ChestSpecificUI.GetComponentInChildren<TMP_Text>().text = Chest1Strings[problemNum];
                    }
                    if (thisChestNum == 2)
                    {
                        ChestSpecificUI.GetComponentInChildren<TMP_Text>().text = Chest2Strings[problemNum];
                    }
                    if (thisChestNum == 3)
                    {
                        ChestSpecificUI.GetComponentInChildren<TMP_Text>().text = Chest3Strings[problemNum];
                    }

                    Chest1.GetComponent<DetectMouseHover>().ChestSpecificUI.GetComponent<Animator>().ResetTrigger("FallBack");
                    Chest2.GetComponent<DetectMouseHover>().ChestSpecificUI.GetComponent<Animator>().ResetTrigger("FallBack");
                    Chest1.GetComponent<DetectMouseHover>().ChestSpecificUI.GetComponent<Animator>().ResetTrigger("Load");
                    Chest2.GetComponent<DetectMouseHover>().ChestSpecificUI.GetComponent<Animator>().ResetTrigger("Load");
                    ChestSpecificUI.GetComponent<Animator>().ResetTrigger("Load");
                    ChestSpecificUI.GetComponent<Animator>().ResetTrigger("FallBack");
                    Chest1.GetComponent<DetectMouseHover>().ChestSpecificUI.GetComponent<Animator>().SetTrigger("FallBack");
                    Chest2.GetComponent<DetectMouseHover>().ChestSpecificUI.GetComponent<Animator>().SetTrigger("FallBack");
                    ChestSpecificUI.GetComponent<Animator>().SetTrigger("Load");
                }
            }
        }
    }

    public void Chest1Func()
    {
        if (correctChest == 1)
        {
            PuzzleUI.SetActive(false);
            roomGeneration.keyRooms--;
            treasureManager.AddTreasure(10);
            Debug.Log("correctamundo");
            Debug.Log(roomGeneration.keyRooms);
            hasCompleted = true;
        }
        else
        {
            PuzzleUI.SetActive(false);
            Debug.Log("oopsie poopsie");
            hasCompleted = true;
        }
    }

    public void Chest2Func()
    {
        if (correctChest == 2)
        {
            PuzzleUI.SetActive(false);
            roomGeneration.keyRooms--;
            treasureManager.AddTreasure(10);
            Debug.Log("correctamundo");
            Debug.Log(roomGeneration.keyRooms);
            hasCompleted = true;
        }
        else
        {
            PuzzleUI.SetActive(false);
            Debug.Log("oopsie poopsie");
            hasCompleted = true;
        }
    }

    public void Chest3Func()
    {
        if (correctChest == 3)
        {
            PuzzleUI.SetActive(false);
            roomGeneration.keyRooms--;
            treasureManager.AddTreasure(10);
            Debug.Log("correctamundo");
            Debug.Log(roomGeneration.keyRooms);
            hasCompleted = true;
        }
        else
        {
            PuzzleUI.SetActive(false);
            Debug.Log("oopsie poopsie");
            hasCompleted = true;
        }
    }
}
