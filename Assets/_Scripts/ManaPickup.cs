using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaPickup : MonoBehaviour
{

    public TMP_Text manaInvText;
    public int manaInvAmt = 1;
    
    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Player"))
         {
            FindObjectOfType<InventoryMaster>().AddManaInv(manaInvAmt);
            InventoryMaster.manaInvNum++;
            Destroy(gameObject);
         }
       
    }
    
}

