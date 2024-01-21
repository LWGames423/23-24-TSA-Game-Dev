using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFunctions : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage;
    // Start is called before the first frame update
    void DamageEnemies(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponentInChildren<EnemyHealthBar>().Change(-attackDamage);
        } 
    }
}
