using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public PlayerManager pm;
    private float _maxHealth;
    private float _currentHealth;
    public TMP_Text healthText;
    
    private static readonly int Flash = Animator.StringToHash("HeartFlashing");
    
    void Start() 
    { 
        
    }
    
    private void Update()
    {

        _maxHealth = pm.maxHealth;
        _currentHealth = pm.currentHealth;
        
        
        healthBar.fillAmount = _currentHealth / _maxHealth;
        

        /*if (InventoryMaster.instance != null)
        {
            healthInvAmount = InventoryMaster.instance.healthAmt;
        }
        else
        {
            Debug.LogError("Inventory Master instance is null lmfao");
        }*/

        
        healthText.text = "Health: " + Decimal.Round((decimal)_currentHealth, 0);
    }
    

    public void HealDamage(float heal)
    {
        _currentHealth += heal;
        healthBar.fillAmount = _currentHealth / 100;
    }

    
}
