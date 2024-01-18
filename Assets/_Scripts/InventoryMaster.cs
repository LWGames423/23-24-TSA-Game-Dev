using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryMaster : MonoBehaviour
{

    public static int manaInvNum = 0;
    public static int healthInvNum = 0;

    public TMP_Text manaInvText;
    public TMP_Text healthInvText;

    public static InventoryMaster instance;
    public int healthAmt;
    public int manaAmt;

    void Awake()
    {
        instance = this;
    }

  

 
   public void AddHealthInv(int healthInvAmt)
   {
        healthAmt += healthInvAmt;
        UpdateHealthInvAmt();
        
   }

   public void DecreaseHealthAmt(int amount)
   {
    healthAmt -= amount;
    UpdateHealthInvAmt();
   }

   void UpdateHealthInvAmt()
   {
    healthInvText.text = "" + healthAmt;
   }

   public void AddManaInv(int manaInvAmt)
   {
    manaAmt += manaInvAmt;
    AddManaInvAmt();
   }

   void AddManaInvAmt()
   {
    manaInvText.text = "" + manaAmt;
   }

   
}
