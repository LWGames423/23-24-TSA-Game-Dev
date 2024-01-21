using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator swordAnimator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage;
    float nextAttackTime = 0f;
    bool canAttack = true;
    void OnFire()
    {
        if(canAttack == true){
            swordAnimator.SetTrigger("Swipe");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponentInChildren<EnemyHealthBar>().Change(-attackDamage);
            } 
            StartCoroutine(AttackCorountine(0.25f));
        }
    }

    IEnumerator AttackCorountine(float seconds)
    {
        canAttack = false;

        yield return new WaitForSeconds(seconds);

        canAttack = true;
    }
}
