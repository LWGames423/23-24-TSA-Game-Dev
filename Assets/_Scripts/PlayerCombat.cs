using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public ParticleSystem disablerParticles;

    public Transform warningCircle;

    public float disablerRange, rebootTime;

    public LayerMask enemyLayer;

    public bool hasDisabler, hasTriggered;

    void Update(){
        if(Input.GetKeyDown(KeyCode.X)){
            if(hasDisabler && hasTriggered){
                hasTriggered = false;
                warningCircle.gameObject.SetActive(false);
                disablerParticles.gameObject.SetActive(true);
                disablerParticles.Play();

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(warningCircle.position, disablerRange, enemyLayer);

                foreach(Collider2D e in hitEnemies)
                {
                    EnemyManager eManager = e.GetComponent<EnemyManager>();
                    Animator eAnim = e.GetComponent<Animator>();

                    if (!eManager.isHacked)
                    {
                        eManager.enabled = false;
                        eManager.isStopped = true;
                        eManager.isAggro = false;
                        eManager.isHacked = true;

                        eAnim.SetTrigger("Hacked");

                        StartCoroutine(delayReboot(rebootTime, e));
                    }
                }
            }

            else if(hasDisabler && !hasTriggered)
            {
                hasTriggered = true;
                warningCircle.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(hasDisabler && hasTriggered)
            {
                hasTriggered = false;
                warningCircle.gameObject.SetActive(false);
            }
        }
    }


    public IEnumerator delayReboot(float seconds, Collider2D e)
    {
        EnemyManager eManager = e.GetComponent<EnemyManager>();
        Animator eAnim = e.GetComponent<Animator>();

        yield return new WaitForSeconds(seconds);

        eManager.enabled = true;
        eManager.isStopped = false;
        eManager.isAggro = true;
        eManager.isHacked = false;

        eAnim.SetTrigger("Reboot");
    }


}
