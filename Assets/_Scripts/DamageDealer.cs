using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    public float dmgAmt = 20f;
    public float dmgCooldown = 3f;
    private bool isCollided = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCollided = true;
            /*StartCoroutine(DealDmg());*/
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isCollided = false;
        }
    }

    /*IEnumerator DealDmg()
    {
       while (isCollided)
       {
        FindObjectOfType<HealthBarController>().TakeDamage(dmgAmt);

        yield return new WaitForSeconds(dmgCooldown);
       }
    }*/
}
