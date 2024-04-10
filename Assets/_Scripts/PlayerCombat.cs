using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    public ParticleSystem disablerParticles;

    public Transform warningCircle, mender, debugger;

    public float disablerRange, rebootTime;

    public LayerMask enemyLayer;

    public bool hasDisabler, hasMender, hasDebugger, hasTriggered, triggeredMender, triggeredDebugger;

    public KeyCode disablerKey, menderKey, debuggerKey;

    public Transform disablerObject, debuggerObject, menderObject;

    void Update(){
        if (hasDebugger)
        {
            foreach(Transform child in debuggerObject)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in debuggerObject)
            {
                child.gameObject.SetActive(false);
            }
        }

        if (hasDisabler)
        {
            foreach (Transform child in disablerObject)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in disablerObject)
            {
                child.gameObject.SetActive(false);
            }
        }

        if (hasMender)
        {
            foreach (Transform child in menderObject)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in menderObject)
            {
                child.gameObject.SetActive(false);
            }
        }


        if (Input.GetKeyDown(disablerKey)){
            if(hasDisabler && hasTriggered){

                triggeredMender = false;
                triggeredDebugger = false;

                hasTriggered = false;

                warningCircle.gameObject.SetActive(false);
                disablerParticles.gameObject.SetActive(true);
                mender.GetComponent<MenderDmgTracker>().isMending = false;
                mender.gameObject.SetActive(false);
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = false;
                debugger.gameObject.SetActive(false);


                disablerParticles.Play();

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(warningCircle.position, disablerRange, enemyLayer);

                foreach(Collider2D e in hitEnemies)
                {
                    if(e.gameObject.GetComponent<WeaponInteractable>() != null)
                    {
                        if(e.gameObject.GetComponent<WeaponInteractable>().interactableType == 1)
                        {
                            e.gameObject.GetComponent<WeaponInteractable>().anim.SetTrigger("Open");
                        }
                    }
                    else
                    {
                        EnemyManager eManager = e.GetComponent<EnemyManager>();
                        EnemyStatsController eStats = e.GetComponent<EnemyStatsController>();
                        Animator eAnim = e.GetComponent<Animator>();

                        if (!eManager.isHacked && eStats.fireWallHealth <= 0)
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
            }

            else if(hasDisabler && !hasTriggered)
            {
                hasTriggered = true;
                warningCircle.gameObject.SetActive(true);

                mender.GetComponent<MenderDmgTracker>().isMending = false;
                mender.gameObject.SetActive(false);
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = false;
                debugger.gameObject.SetActive(false);
            }
        }

        //Debugger

        if (Input.GetKeyDown(debuggerKey))
        {
            if (hasDebugger && !triggeredDebugger)
            {
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = false;
                debugger.gameObject.SetActive(true);
                mender.GetComponent<MenderDmgTracker>().isMending = false;
                mender.gameObject.SetActive(false);
                hasTriggered = false;
                warningCircle.gameObject.SetActive(false);
                triggeredMender = false;
                triggeredDebugger = true; 
            }
            else if (hasDebugger && triggeredDebugger)
            {
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = true;
                debugger.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyUp(debuggerKey))
        {
            if (hasDebugger && triggeredDebugger)
            {
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = false;
            }
        }

        //Mender

        if (Input.GetKeyDown(menderKey))
        {
            if (hasMender && !triggeredMender)
            {
                mender.gameObject.SetActive(true);
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = false;
                debugger.gameObject.SetActive(false);
                hasTriggered = false;
                warningCircle.gameObject.SetActive(false);
                triggeredMender = true;
                triggeredDebugger = false;
            }
            else if (hasMender && triggeredMender)
            {
                mender.GetComponent<MenderDmgTracker>().isMending = true;
                mender.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyUp(menderKey))
        {
            if (hasMender && triggeredMender)
            {
                mender.GetComponent<MenderDmgTracker>().isMending = false;

            }
        }





        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(hasDisabler && hasTriggered)
            {
                hasTriggered = false;
                warningCircle.gameObject.SetActive(false);
            }
            if (hasDebugger)
            {
                triggeredDebugger = false;
                debugger.GetComponent<DebuggerDmgTracker>().isVacuuming = false;
                debugger.gameObject.SetActive(false);
            }
            if (hasMender)
            {
                triggeredMender = false;
                mender.GetComponent<MenderDmgTracker>().isMending = false;
                mender.gameObject.SetActive(false);
            }
        }
    }


    public IEnumerator delayReboot(float seconds, Collider2D e)
    {
        EnemyManager eManager = e.GetComponent<EnemyManager>();
        Animator eAnim = e.GetComponent<Animator>();

        yield return new WaitForSeconds(seconds);

        if(eManager != null)
        {
            eManager.enabled = true;
            eManager.isStopped = false;
            eManager.isAggro = true;
            eManager.isHacked = false;

            eAnim.SetTrigger("Reboot");
        }
    }


}
