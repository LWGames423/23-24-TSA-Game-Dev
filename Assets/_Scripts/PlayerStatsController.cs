using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    public bool isInvul;
    public float invulFrames;
    public float health, maxHealth, stamina, maxStamina;
    public PlayerManager pManager;
    public Image corruptMeter;

    void Start()
    {
        pManager = transform.GetComponent<PlayerManager>();
        health = maxHealth;
        stamina = maxStamina;
        maxHealth = pManager.maxHealth;
        maxStamina = pManager.stamina;
    }

    void Update()
    {
        pManager.currentHealth = health;
        pManager.stamina = stamina;
        corruptMeter.fillAmount = (maxHealth - health)/maxHealth;
    }

    public void Damage(float damage)
    {
        if (!isInvul)
        {
            health = health - damage;
            StartCoroutine(delayInvul(invulFrames));
        }
    }

    public IEnumerator delayInvul(float seconds)
    {
        isInvul = true;
        yield return new WaitForSeconds(seconds);
        isInvul = false;
    }

}