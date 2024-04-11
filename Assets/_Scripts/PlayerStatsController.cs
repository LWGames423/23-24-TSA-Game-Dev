using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    public bool isInvul;
    public float invulFrames;
    public float health, maxHealth, stamina, maxStamina;
    public PlayerManager pManager;
    public Image corruptMeter;
    
    public Animator fade;

    private float _waitTime = 2.2f;
    
    void Start()
    {
        pManager = transform.GetComponent<PlayerManager>();
        health = maxHealth;
        stamina = maxStamina;
        maxHealth = pManager.maxHealth;
        maxStamina = pManager.stamina;
        isInvul = pManager.isInvuln;
    }

    void Update()
    {
        pManager.currentHealth = health;
        pManager.stamina = stamina;
        corruptMeter.fillAmount = (maxHealth - health)/maxHealth;
        isInvul = pManager.isInvuln;

        if (health <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        fade.SetTrigger("FadeOut");
        _waitTime -= Time.deltaTime;
        if (_waitTime <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
