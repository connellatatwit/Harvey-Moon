using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyShooterStates
{
    Idle,
    SeePlayer
}

public class GunEnemy : MonoBehaviour, Enemy
{
    [SerializeField] int health;
    private int currentHealth;

    [SerializeField] Image hpBar;
    [Header("Shooting State")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePos;
    [SerializeField] float fireCd;
    private float fireTimer;
    [SerializeField] float dodgeCd;
    private float dodgeTimer;
    [SerializeField] float dodgeSpeed;

    [SerializeField] int gunDamage;
    [SerializeField] float bulletSpeed;

    [SerializeField] float maxError;
    private bool shooting = false;

    [Header("Idle walking")]
    [SerializeField] float walkCd;
    private float walkTimer;
    [SerializeField] float walkSpeed;

    [Header("Other")]
    [SerializeField] float sightDist;

    [SerializeField] EnemyShooterStates state;

    [SerializeField] LayerMask player;
    [SerializeField] LayerMask wall;

    private Transform playerTran;
    private Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        walkTimer = Random.Range(0, walkCd+1);
        fireTimer = fireCd;
        dodgeTimer = Random.Range(1, dodgeCd+1);
        rb = GetComponent<Rigidbody2D>();

        currentHealth = health;
        hpBar.fillAmount = 1;
    }

    private void FixedUpdate()
    {
        Vector2 dir = playerTran.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, sightDist, player | wall);

        if(hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                state = EnemyShooterStates.SeePlayer;
                anim.SetBool("SeePlayer", true);
                if (transform.position.x - hit.collider.transform.position.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                Debug.Log("Blocked");
                state = EnemyShooterStates.Idle;
                anim.SetBool("SeePlayer", false);
            }
        }
    }

    private void Update()
    {
        if (state == EnemyShooterStates.SeePlayer)
        {
            fireTimer -= Time.deltaTime;
            dodgeTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                fireTimer = fireCd;
                anim.SetTrigger("Shoot");
                shooting = true;
            }
            if(dodgeTimer <= 0 && !shooting)
            {
                dodgeTimer = dodgeCd;
                anim.SetTrigger("Dodge");
            }
        }
        else if(state == EnemyShooterStates.Idle)
        {
            walkTimer -= Time.deltaTime;
            if(walkTimer <= 0)
            {
                walkTimer = walkCd;
                anim.SetTrigger("Walk");
            }
        }
    }

    public void Walk()
    {
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);

        Vector2 walkDir = new Vector2(x,y) * walkSpeed;

        rb.velocity = walkDir;
        if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Dodge()
    {
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);

        Vector2 dodgeDir = new Vector2(x, y) * dodgeSpeed;

        rb.velocity = dodgeDir;
    }

    public void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firePos.position;

        float error = Random.Range(-maxError, maxError);
        int xOry = Random.Range(0, 2);
        Vector2 errorPos = playerTran.position;

        if (xOry == 0) // X
        {
            errorPos = new Vector2(errorPos.x + error, errorPos.y);
        }
        else // Y
        {
            errorPos = new Vector2(errorPos.x, errorPos.y + error);
        }


        bullet.GetComponent<EnemyBullet>().InitBullet(errorPos, gunDamage, bulletSpeed);
    }

    public void DoneShoting()
    {
        shooting = false;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            
            if(gameObject.GetComponent<Collider2D>() != null)
                gameObject.GetComponent<Collider2D>().enabled = false;
        }
        hpBar.fillAmount = (float)currentHealth / (float)health;
    }
}
