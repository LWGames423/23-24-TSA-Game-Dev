using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public Image effect;
    public PlayerManager pm;

    private float _maxHealth;
    private float _currentHealth;
    
    private void Start()
    {
        _maxHealth = pm.maxHealth;
        _currentHealth = pm.currentHealth;
    }

    private void Update()
    {
        _currentHealth = pm.currentHealth;
        effect.color = new Color(effect.color.r, effect.color.r, effect.color.r, (_maxHealth - _currentHealth)/_maxHealth);
    }
}
