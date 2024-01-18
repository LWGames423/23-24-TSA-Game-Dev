using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarControler : MonoBehaviour
{
    #region Health Bar Assets
    public Image HealthBar;
    public float healthAmount = 100f;
    public TMP_Text healthText;
    #endregion

    #region Blinking Assets
   public Image heartIcon;
   public Animator _anim;
   bool _isMortallyInjured = false;
   private static readonly int Flash = Animator.StringToHash("HeartFlashing");
   #endregion
   
   public int  healthInvAmount;

   void Start()
   {
    /*
    if (InventoryMaster.instance != null)
    {
        healthInvAmount = InventoryMaster.instance.healthAmt;
        Debug.Log("Initial healthInvAmount: " + healthInvAmount);
    }
    else
    {
        Debug.LogError("Inventory Master instance is null lmfao");
    }

    */
   }

    private bool isMortallyInjured
    {
        set
        {
            _isMortallyInjured = value;
            _anim.SetBool(Flash, _isMortallyInjured);
        }
    }
   private void Update()
    {

        if (InventoryMaster.instance != null)
    {
        healthInvAmount = InventoryMaster.instance.healthAmt;
        Debug.Log("Initial healthInvAmount: " + healthInvAmount);
    }
    else
    {
        Debug.LogError("Inventory Master instance is null lmfao");
    }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

        

        if (Input.GetKeyDown(KeyCode.F))
        {
             Debug.Log("Key pressed");
            if (healthInvAmount > 0)
            {
                HealDamage(20);
                healthInvAmount--;
                InventoryMaster.instance.DecreaseHealthAmt(1);
                Debug.Log("Decreased healthInvAmount. New value: " + healthInvAmount);
            }
            
            
        }
       
        

        

        healthText.text = "Health: " + healthAmount;

        if(healthAmount <= 50f)
        {
            isMortallyInjured = true;
        }
        else
        {
            isMortallyInjured = false;
        }

    }
    
    public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        HealthBar.fillAmount = healthAmount / 100;

    }

    public void HealDamage(float Heal)
    {
        healthAmount += Heal;
        HealthBar.fillAmount = healthAmount / 100;
    }

    
}
