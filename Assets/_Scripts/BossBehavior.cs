using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    #region Variables

    [Header("Boss Variables")]
    public Slider enemyHealth;
    //add anim here
    public GameObject enemyHitbox;
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    private EnemyAttacks enemyAttacks;

    [Header("Attack Points and Info")]
    public Transform[] points;
    public Transform castPoint, maxPoint, attackPoint, hitPoint;

    public float attackRange;
    public LayerMask playerLayer;
    public int pointDest;

    [Header("Booleans")]
    public bool isStopped;
    public bool isAggro;
    public bool isHacked;

    [Header("Misc. Player and Self Info")]
    private Transform playerTransform;
    private float ogScaleX;
    private float ogPlayerSpeed;

    [Header("Damage and Force")]
    public float collideDamage;
    public float slashDamage;

    public float knockForce;
    public float chargeForce;

    [Header("Timing")]
    public float chargeCooldown;
    private float currentCooldownTime;
    private bool canCharge = true;

    [Header("AI")]
    public float slashDistance;
    public LayerMask groundLayer;
    public Vector2 dashBackSpeed = new Vector2(1.0f, 1.0f);
    public float walkSpeed;
    private int phase = 0;
    private int stage = 0;
    private float distanceToPlayer;

    #endregion

    private void Awake()
    {
        phase = 1; stage = 1;
    }

    void Update()
    {
        distanceToPlayer = Mathf.Sqrt(Mathf.Pow(playerTransform.position.x - transform.position.x, 2.0f) + Mathf.Pow(playerTransform.position.x - transform.position.x, 2.0f));
    }

    private void Phase1Stage1()
    {
        for (int i = 0; i < 2;)
        {
            if (distanceToPlayer > slashDistance)
            {
                rb.velocity = (playerTransform.position - transform.position).normalized * walkSpeed;

            }
            else if (distanceToPlayer <= slashDistance)
            {
                float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);

                //attack

                rb.velocity = new Vector3(-direction * dashBackSpeed.x, dashBackSpeed.y, 0.0f);

                i++;
            }
        }

        ChangeStage(1, 1);
    }

    private void Phase1Stage2()
    {
        float playerVel = playerTransform.gameObject.GetComponent<Rigidbody2D>().velocity.x;

        transform.position = playerTransform.position + new Vector3 (Mathf.Sign(playerVel) * 10.0f, 0.0f, 0.0f);
        if (Physics2D.OverlapBox(transform.position, new Vector2(1.0f, 1.0f), 0, groundLayer))
        {
            transform.position = playerTransform.position - new Vector3(Mathf.Sign(playerVel) * 10.0f, 0.0f, 0.0f);
        }

        //attack

        ChangeStage(1, 2);
    }

    private void Phase2Stage1()
    {
        for (int i = 0; i < 2;)
        {
            if (distanceToPlayer > slashDistance)
            {
                rb.velocity = (playerTransform.position - transform.position).normalized * walkSpeed;

            }
            else if (distanceToPlayer <= slashDistance)
            {

                float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);

                //attack

                rb.velocity = new Vector3(-direction * dashBackSpeed.x, dashBackSpeed.y, 0.0f);

                i++;
            }
        }

        ChangeStage(2, 1);
    }

    private void Phase2Stage2()
    {

        float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);

        //attack
        //stuck

        rb.velocity = new Vector3(-direction * dashBackSpeed.x, dashBackSpeed.y, 0.0f);

        ChangeStage(2, 2);
    }

    private void Phase2Stage3()
    {
        float playerVel = playerTransform.gameObject.GetComponent<Rigidbody2D>().velocity.x;

        transform.position = playerTransform.position + new Vector3(Mathf.Sign(playerVel) * 10.0f, 0.0f, 0.0f);
        if (Physics2D.OverlapBox(transform.position, new Vector2(1.0f, 1.0f), 0, groundLayer))
        {
            transform.position = playerTransform.position - new Vector3(Mathf.Sign(playerVel) * 10.0f, 0.0f, 0.0f);
        }

        //attack but faster

        ChangeStage(2, 3);
    }

    private void ChangeStage(int phase, int stage)
    {
        if (phase == 1)
        {
            if (stage == 1)
            {
                Phase1Stage2();
            }

            else if (stage == 2)
            {
                Phase1Stage1();
            }
        }

        else if (phase == 2)
        {
            if (stage == 1)
            {
                Phase2Stage2();
            }

            else if (stage == 2)
            {
                Phase2Stage3();
            }

            else if (stage == 3)
            {
                Phase2Stage1();
            }
        }
    }

    private void ChangePhase()
    {
        //phase change anim

        Phase2Stage1();
    }

    public void DamagePlayer(Transform player, float damage)
    {
        if (transform.position.x <= player.position.x)
        {
            if (!player.GetComponent<PlayerStatsController>().isInvul)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(knockForce, knockForce);
                player.GetComponent<PlayerStatsController>().Damage(damage);
            }
        }
        else if (transform.position.x > player.position.x)
        {
            if (!player.GetComponent<PlayerStatsController>().isInvul)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-knockForce, knockForce);
                player.GetComponent<PlayerStatsController>().Damage(damage);
            }
        }
    }

    public void SwipeCollide()
    {
        Collider2D hitPlayers = Physics2D.OverlapCircle(hitPoint.position, attackRange, playerLayer);

        if (hitPlayers != null)
        {
            if (hitPlayers.transform.name == "Player")
            {
                DamagePlayer(hitPlayers.transform, slashDamage);
            }
        }
    }
}
