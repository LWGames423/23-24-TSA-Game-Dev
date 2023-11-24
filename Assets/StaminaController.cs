using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StaminaController : MonoBehaviour
{
    public Image stamBar;
    public float stamAmount = 100f;
    public TMP_Text stamText;


   private void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.H))
        {
            LoseStamina(20);
        }

        stamText.text = "Stamina: " + stamAmount;
    }
    
    public void LoseStamina(float Stamina)
    {
        stamAmount -= Stamina;
        stamBar.fillAmount = stamAmount / 100;

    }
}
