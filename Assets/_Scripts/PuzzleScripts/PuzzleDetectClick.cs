using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PuzzleDetectClick : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite hoverSprite, regSprite;
    public bool isOnSprite, isInstructions;
    public bool hasCompleted = false;
    public GameObject twentyFourGame;

    private void Awake()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
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
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnSprite)
            {
                if (isInstructions)
                {
                    if (!hasCompleted)
                    {
                        twentyFourGame.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("you've already done this puzzle");
                    }
                }
            }
        }
    }
}
