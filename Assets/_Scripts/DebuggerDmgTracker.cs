using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerDmgTracker : MonoBehaviour
{
    public bool isVacuuming;

    public float damagePerFrame;
    public ParticleSystem bugSystem;

    // Update is called once per frame
    void Update()
    {
        if (isVacuuming)
        {
            foreach(Transform child in this.transform)
            {
                if (child != bugSystem.transform)
                {
                    if (!child.gameObject.GetComponent<ParticleSystem>().isEmitting)
                    {
                        child.gameObject.GetComponent<ParticleSystem>().Play();
                    }
                }
            }
        }
        else if (!isVacuuming)
        {
            foreach (Transform child in this.transform)
            {
                if (child.gameObject.GetComponent<ParticleSystem>().isEmitting)
                {
                    child.gameObject.GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmitting);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isVacuuming)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("chat i'm not cooking");
                if (collision.gameObject.GetComponent<WeaponInteractable>() != null)
                {
                    Debug.Log("chat i'm not cooking");
                    if(collision.gameObject.GetComponent<WeaponInteractable>().interactableType == 3)
                    {
                        collision.gameObject.GetComponent<WeaponInteractable>().Damage(damagePerFrame);
                    }
                }
                else
                {
                    GameObject enemy = collision.gameObject;
                    EnemyStatsController enemyStats = enemy.GetComponent<EnemyStatsController>();
                    if (enemyStats.fireWallHealth > 0)
                    {
                        enemyStats.Damage(damagePerFrame);
                    }
                    if (!bugSystem.isEmitting)
                    {
                        bugSystem.Play();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isVacuuming)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (bugSystem.isEmitting)
                {
                    bugSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                }
            }
        }
    }
}
