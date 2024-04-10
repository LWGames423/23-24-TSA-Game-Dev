using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractable : MonoBehaviour
{
    public int interactableType; //1 for breachable door and 2 for enemy door, 3 for vacuum+breachable door
    public List<GameObject> doorEnemies = new List<GameObject>();
    public Animator anim;

    public Transform fireWallBar;
    public float ogScaleX, fireWallHealth, maxWallHealth;
    public Transform doorSprite;
    public Color breachableColor;

    private void Start()
    {
        ogScaleX = fireWallBar.localScale.x;

        if(GetComponentInParent<Animator>() != null)
        {
            anim = GetComponentInParent<Animator>();
        }
        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in doorEnemies)
        {
            if (enemy == null)
            {
                doorEnemies.Remove(enemy);
            }
        }
        if (interactableType == 2)
        {
            if(doorEnemies.Count == 0)
            {
                anim.SetTrigger("Open");
            }
        }
        if(interactableType == 3)
        {
            fireWallBar.localScale = new Vector3((fireWallHealth * ogScaleX / maxWallHealth), fireWallBar.localScale.y, fireWallBar.localScale.z);

        }
    }

    public void Damage(float damage)
    {
        if(interactableType == 3)
        {
            if ((fireWallHealth - damage) <= 0)
            {
                fireWallHealth = 0;
                fireWallBar.transform.parent.gameObject.SetActive(false);
                doorSprite.GetComponent<SpriteRenderer>().color = breachableColor;
                interactableType = 1;
            }
            else
            {
                fireWallHealth = fireWallHealth - damage;
            }
        }
        
    }
}
