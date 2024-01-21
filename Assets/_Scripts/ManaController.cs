using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    [FormerlySerializedAs("stamBar")] public Image manaBar;
    [FormerlySerializedAs("maxStam")] public float maxMana = 100f;
    [FormerlySerializedAs("currentStam")] public float currentMana;
    [FormerlySerializedAs("stamText")] public TMP_Text manaText;

    public float regenRate = 2f;
    private float _elapsed;
    public float regenDelay;
    

    private void Awake()
    {
        currentMana = maxMana;
        _elapsed = 0f;
        regenDelay = 1f;
    }

    private void Update()
    {
        RegenMana();
        manaText.text = "Stamina: " + Decimal.Round((decimal)currentMana, 0);
        manaBar.fillAmount = currentMana / 100;
    }

    public void RegenMana()
    {
        if (_elapsed > regenDelay)
        {
            currentMana = Mathf.Min(currentMana + regenRate * Time.deltaTime, maxMana);
        }
        _elapsed += Time.deltaTime;

    }

    public void LoseMana(float stamina)
    {
        _elapsed = 0f;
        currentMana = Mathf.Max(currentMana - stamina, 0);
    }
}
