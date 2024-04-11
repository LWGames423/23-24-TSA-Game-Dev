using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverEnemyScript : MonoBehaviour
{
    private bool isAggro;
    public Transform castPoint, maxPoint;
    private Rigidbody2D rb;
    public Animator anim;
    public Transform explosionPrefab;

    public float attackRange;
    public LayerMask playerLayer;
    private Transform playerTransform;
    public float diveDamage;
    public float knockForce;

    public float ogPlayerSpeed;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        ogPlayerSpeed = playerTransform.GetComponent<PlayerManager>().moveSpeed;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAggro)
        {
            RaycastHit2D hit = Physics2D.Linecast(castPoint.position, maxPoint.position, 1 << LayerMask.NameToLayer("ground") | 1 << LayerMask.NameToLayer("player"));
            if (hit.collider != null)
            {
                if (hit.collider.name == "Player" || hit.collider.name == "Player 1")
                {
                    initAnger();
                }
            }
        }
    }

    public void initAnger()
    {
        anim.SetTrigger("Dive");
    }

    public void Dive()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        rb.AddForce(new Vector2(0, -25), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Collider2D hitPlayers = Physics2D.OverlapCircle(castPoint.position, attackRange, playerLayer);

        if (hitPlayers != null)
        {
            if (hitPlayers.transform.name == "Player")
            {
                DamagePlayer(playerTransform, diveDamage);
            }
        }

        this.transform.parent.gameObject.SetActive(false);
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

}
