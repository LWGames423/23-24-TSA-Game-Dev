using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    #region Variables

    [Header("General Enemy Variables")]
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
    public bool isBeetle;

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

    #endregion

    private void Start()
    {
        enemyAttacks = GetComponent<EnemyAttacks>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ogScaleX = transform.localScale.x;
        playerTransform = FindObjectOfType<PlayerManager>().transform;
        ogPlayerSpeed = playerTransform.GetComponent<PlayerManager>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeetle)
        {
            if (!isAggro)
            {
                RaycastHit2D hit = Physics2D.Linecast(castPoint.position, maxPoint.position, 1 << LayerMask.NameToLayer("ground") | 1 << LayerMask.NameToLayer("player"));
                if (hit.collider != null)
                {
                    if (hit.collider.name == "Player" || hit.collider.name == "Player 1")
                    {
                        isStopped = true;
                        initAnger();
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                }
            }

            if (!isStopped && !isAggro)
            {
                if (pointDest == 0)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                    if (Mathf.Abs(transform.position.x - points[0].position.x) < 0.5f)
                    {
                        pointDest = 1;
                        Flip();
                    }
                }
                if (pointDest == 1)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    if (Mathf.Abs(transform.position.x - points[1].position.x) < 0.5f)
                    {
                        pointDest = 0;
                        Flip();
                    }
                }
            }

            if (!isStopped && isAggro)
            {
                if (transform.position.x > playerTransform.position.x)
                {
                    transform.localScale = new Vector3(ogScaleX, transform.localScale.y, transform.localScale.z);
                    rb.velocity = new Vector2(-speed, rb.velocity.y);

                }
                else if (transform.position.x < playerTransform.position.x)
                {
                    transform.localScale = new Vector3(-ogScaleX, transform.localScale.y, transform.localScale.z);
                    rb.velocity = new Vector2(speed, rb.velocity.y);

                }

                RaycastHit2D hit = Physics2D.Linecast(castPoint.position, attackPoint.position, 1 << LayerMask.NameToLayer("ground") | 1 << LayerMask.NameToLayer("player"));
                if (hit.collider != null)
                {
                    if (hit.collider.name == "Player")
                    {
                        isStopped = true;
                        initAttack();
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                    
                }
                else if (hit.collider == null && canCharge)
                {
                    enemyAttacks.ChargeAttack(anim, chargeForce);
                    canCharge = false;
                    currentCooldownTime = chargeCooldown;
                }

                if (!canCharge)
                {
                    currentCooldownTime -= Time.deltaTime;
                }
                if (currentCooldownTime <= 0)
                {
                    canCharge = true;
                }
            }
        }
    }

    public void initAnger()
    {
        anim.SetTrigger("Anger");
        speed = speed * 1.5f;
        isAggro = true;
    }

    public void initAttack()
    {
        StopEnemy();
        SwipeAttack();
    }

    public void StopEnemy()
    {
        isStopped = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void UnStopEnemy()
    {
        isStopped = false;
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHacked)
        {
            if (collision != null)
            {
                if (collision.gameObject.name == "Player")
                {
                    Transform player = collision.gameObject.transform;
                    DamagePlayer(player, collideDamage);
                }
            }
        }
    }

    
    public IEnumerator delaySpeedRegain(float seconds, Transform player)
    {
        yield return new WaitForSeconds(seconds);
        player.GetComponent<PlayerManager>().moveSpeed = ogPlayerSpeed;
    }

    public void SwipeAttack()
    {
        anim.SetTrigger("Attack");
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
