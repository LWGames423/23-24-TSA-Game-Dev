using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{

    public TMP_Text healthInvText;
    public int healthInvAmt = 1;
    
    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Player"))
         {
            FindObjectOfType<InventoryMaster>().AddHealthInv(healthInvAmt);
            InventoryMaster.healthInvNum++;
            Destroy(gameObject);
         }
       
    }
    
}
