using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public bool isInvul;
    public float invulFrames;
    public float health, maxHealth, stamina, maxStamina;
    public PlayerManager pManager;

    void Start()
    {
        pManager = transform.GetComponent<PlayerManager>();
        maxHealth = pManager.health;
        maxStamina = pManager.health;
    }

    void Update()
    {
        pManager.health = health;
        pManager.stamina = stamina;
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
