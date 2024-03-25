using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenderDmgTracker : MonoBehaviour
{
    public bool isMending;

    public float damagePerFrame;

    private void Update()
    {
        if (isMending)
        {
            this.GetComponent<Animator>().SetTrigger("Mend");
        }
        else if (!isMending)
        {
            this.GetComponent<Animator>().SetTrigger("StopMend");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isMending)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject enemy = collision.gameObject;
                EnemyStatsController enemyStats = enemy.GetComponent<EnemyStatsController>();
                if (enemyStats.fireWallHealth <= 0)
                {
                    enemyStats.Damage(damagePerFrame);
                }
            }
        }
    }
}
