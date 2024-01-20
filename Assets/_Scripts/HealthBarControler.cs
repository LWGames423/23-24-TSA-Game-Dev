using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class HealthBarControler : MonoBehaviour
{
    #region Health Bar Assets
    public Image HealthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public TMP_Text healthText;
    #endregion

    #region Blinking Assets
   public Image heartIcon;
   public Animator _anim;
   bool _isMortallyInjured = false;
   private static readonly int Flash = Animator.StringToHash("HeartFlashing");
   #endregion

   public float regenRate = 5f;
   private float timeElapsed;
   public float regenDelay = 2f;

   
   public int  healthInvAmount;

   

   void Start()
   {
    currentHealth = maxHealth;
    timeElapsed = Time.time;
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

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (Time.time - timeElapsed > regenDelay)
        {
            RegenHealth();
        }

        if (InventoryMaster.instance != null)
        {
            healthInvAmount = InventoryMaster.instance.healthAmt;
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
            if (healthInvAmount > 0 && currentHealth < 100f)
            {
                HealDamage(20);
                healthInvAmount--;
                InventoryMaster.instance.DecreaseHealthAmt(1);
            }
            
            
        }
       

        healthText.text = "Health: " + Decimal.Round((decimal)currentHealth, 0);

        if(currentHealth <= 50f)
        {
            isMortallyInjured = true;
        }
        else
        {
            isMortallyInjured = false;
        }

    }

    

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthBar.fillAmount = currentHealth / 100;
        timeElapsed = Time.time;
    }

    void RegenHealth()
    {
        currentHealth += Time.deltaTime * regenRate;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        HealthBar.fillAmount = currentHealth / 100;
    }

    public void HealDamage(float heal)
    {
        currentHealth += heal;
        HealthBar.fillAmount = currentHealth / 100;
    }

    
}
