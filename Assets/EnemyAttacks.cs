using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    private GameObject bug;
    private Transform playerTransform;
    public float baseThrustPower, ogScaleX;
    private bool isFacingRight;

    private void Start()
    {
        bug = this.gameObject;
        ogScaleX = transform.localScale.x;
        playerTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void ChargeAttack(Animator anim, float putPower)
    {
        EnemyManager enemyManager = bug.GetComponent<EnemyManager>();

        enemyManager.StopEnemy();

        anim.SetTrigger("Charge");
        baseThrustPower = putPower;
        if (transform.position.x > playerTransform.position.x)
        {
            isFacingRight = false;
        }
        else if (transform.position.x < playerTransform.position.x)
        {
            isFacingRight = true;
        }
    }

    public void ImpulseForceEnemy()
    {
        Rigidbody2D bugRB = bug.GetComponent<Rigidbody2D>();

        if (!isFacingRight)
        {
            transform.localScale = new Vector3(ogScaleX, transform.localScale.y, transform.localScale.z);
            bugRB.velocity = new Vector2(-baseThrustPower, bugRB.velocity.y);
        }
        else if (isFacingRight)
        {
            transform.localScale = new Vector3(-ogScaleX, transform.localScale.y, transform.localScale.z);
            bugRB.velocity = new Vector2(baseThrustPower, bugRB.velocity.y);

        }

    }


}
