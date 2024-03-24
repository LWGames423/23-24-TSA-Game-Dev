using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
    public float maxHealth, currentHealth, maxWallHealth, fireWallHealth;
    public bool isHacked, isFireWallCleared;
    public float yOffset;
    public Transform fireWallBar, healthBar, barsParent;
    public Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        fireWallHealth = maxWallHealth;
        enemyAnimator = GetComponent<Animator>();
        yOffset = barsParent.position.y-transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Damage(10);
        }
        fireWallBar.localScale = new Vector3((fireWallHealth*20 / maxWallHealth), fireWallBar.localScale.y, fireWallBar.localScale.z);
        healthBar.localScale = new Vector3((currentHealth*20 / maxHealth), healthBar.localScale.y, healthBar.localScale.z);

        barsParent.position = new Vector3(transform.position.x, transform.position.y + yOffset, barsParent.position.z);
    }

    public void Damage(float damage)
    {
        if (isFireWallCleared)
        {
            if ((currentHealth - damage) <= 0)
            {
                currentHealth = 0;
                DefeatEnemy();
            }
            else
            {
                currentHealth = currentHealth - damage;
            }
        }
        else
        {
            if ((fireWallHealth - damage) <= 0)
            {
                fireWallHealth = 0;
                isFireWallCleared = true;
                fireWallBar.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                fireWallHealth = fireWallHealth - damage;
            }
        }
    }

    public void DefeatEnemy()
    {
        Destroy(transform.parent.gameObject);
    }
}
