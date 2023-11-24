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

   void Start()
   {
    
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
       /* if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);

        }
*/
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
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

    /*public void HealingBlue(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        HBB.fillAmount = healthAmount / 100;
    }
*/
}
