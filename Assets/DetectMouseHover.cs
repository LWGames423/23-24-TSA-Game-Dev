using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMouseHover : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite hoverSprite, regSprite;
    public bool isOnSprite;
    public GameObject PuzzleUI;

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
        if (Input.GetMouseButtonDown(0)){
            if (isOnSprite)
            {
                PuzzleUI.SetActive(true);
            }
        }
    }
}
