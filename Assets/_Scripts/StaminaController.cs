using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class StaminaController : MonoBehaviour
{
    public Image stamBar;
    [FormerlySerializedAs("stamAmount")] public float maxStam = 100f;
    public float currentStam;
    public TMP_Text stamText;
    
    public float regenRate = 2f;
    private float _elapsed;
    public float regenDelay;
    
    private void Awake()
    {
        currentStam = maxStam;
        _elapsed = 0f;
        regenDelay = 1f;
    }

    private void Update()
    {
        RegenStamina();
        stamText.text = "Stamina: " + Decimal.Round((decimal)currentStam, 0);
    }

    public void RegenStamina()
    {
        if (_elapsed > regenDelay)
        {
            currentStam = Mathf.Min(currentStam + regenRate * Time.deltaTime, maxStam);
        }    
        _elapsed += Time.deltaTime;

    }
    
    public void LoseStamina(float stamina)
    {
        _elapsed = 0f;
        currentStam = Mathf.Max(currentStam - stamina, 0);
        stamBar.fillAmount = currentStam / 100;
    }
}
