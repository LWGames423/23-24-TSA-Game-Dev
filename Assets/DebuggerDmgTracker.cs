using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerDmgTracker : MonoBehaviour
{
    public bool isVacuuming;

    public float damagePerFrame;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isVacuuming)
        {
            if (collision.GetComponent<Collider>().gameObject.tag == "Enemy")
            {
                GameObject enemy = collision.GetComponent<Collider>().gameObject;
                EnemyStatsController enemyStats = enemy.GetComponent<EnemyStatsController>();
                enemyStats.Damage(damagePerFrame);
            }
        }
    }
}
