using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrotherSunState
{
    Sleeping,
    SlowBounce,
    Aiming,
    FastBounce,
    Locked,
    Busy
}
public class BrotherSun : MonoBehaviour, Enemy
{
    [Header("Unity Information")]
    [SerializeField] BrotherSunState state;
    [SerializeField] Transform topSensor;
    [SerializeField] Transform rightSensor;
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform bottomSensor;
    [SerializeField] float sensorRadius;
    [SerializeField] LayerMask wallLayer;
    private bool isTouchingUp, isTouchingDown, isTouchingRight, isTouchingLeft; // = Physicis2d.OverlapCircle(position, sensorRadius, wallLayer)
    private bool goingRight, goingUp;
    private Rigidbody2D rb;
    private Transform player;

    [Header("Basic Stats")]
    [SerializeField] int maxHealth;
    private int currentHealth;
    private bool halfHealth = false;
    bool fastNext = true;

    [Header("SlowBounce")]
    [SerializeField] float slowSpeed;
    [SerializeField] Vector2 slowBounceDir;

    [Header("FastBounce")]
    [SerializeField] float fastSpeed;
    [SerializeField] float fastBounceCd;
    private float fastBounceTimer;
    Coroutine aimingRoutine;
    private Vector2 fireDir;
    private bool debugFastBounce = false;

    [Header("LockedState")]
    [SerializeField] int numbSweeps;
    [SerializeField] float lockedCd;
    private float lockedTimer;

    private void Start()
    {
        InitBoss();
    }

    public void InitBoss()
    {
        slowBounceDir.Normalize();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        goingUp = true;
        goingRight = false;

        fastBounceTimer = fastBounceCd;
        lockedTimer = lockedCd;
    }
    public void TakeDamage(int amount)
    {
        if (!halfHealth)
        {
            if (maxHealth / 2 > currentHealth)
            {
                // At half health
                halfHealth = true;
                //Increase stats
            }
        }
    }

    /*
     * When Hit a wall and deciding how to turn, only change the variable that touched. So if it is 1,1 and you hit a left or right wall, then change the x, so it becomes negative 
     * and random between 0-1, maybe only random while fast.
     */
    private void Update()
    {
        HandleSensors();
        /*
         * When going into fast become busy and start animation
          * When going into Locked become busy and start animation
          */
        if(state == BrotherSunState.SlowBounce)
        {
            /*
             * Scan for walls, and bounce off of them
             * Fire Fireball projectiles at player
             * Fire burst of firebals at player
             */
            SlowBounce();

            if (fastNext)
            {
                fastBounceTimer -= Time.deltaTime;
                if (fastBounceTimer <= 0)
                {
                    state = BrotherSunState.Aiming;
                    fastBounceTimer = fastBounceCd;
                    //fastNext = !fastNext;
                }
            }
            else
            {
                lockedTimer -= Time.deltaTime;
                if(lockedTimer <= 0)
                {
                    state = BrotherSunState.Locked;
                    lockedTimer = lockedCd;
                    fastNext = !fastNext;
                }
            }
        }
        else if(state == BrotherSunState.Aiming)
        {
            if (TouchingAny() && aimingRoutine == null)
            {
                aimingRoutine = StartCoroutine(AimingTimer());
            }
        }
        else if (state == BrotherSunState.FastBounce)
        {
            /*
             * Scan for walls and pause when hit wall.
             * Aim body at player when hit wall and then fire self at players drection
             */
            
            FastBounce();

            if (TouchingAny() && debugFastBounce)
            {
                state = BrotherSunState.Aiming;
            }
        }
        else if (state == BrotherSunState.Locked)
        {
            /*
             * Fire a laser that sweeps left to right and then another from right to left
             */
        }
    }
    void HandleSensors()
    {
        isTouchingUp = Physics2D.OverlapCircle(topSensor.position, sensorRadius, wallLayer);
        isTouchingDown = Physics2D.OverlapCircle(bottomSensor.position, sensorRadius, wallLayer);
        isTouchingRight = Physics2D.OverlapCircle(rightSensor.position, sensorRadius, wallLayer);
        isTouchingLeft = Physics2D.OverlapCircle(leftSensor.position, sensorRadius, wallLayer);
    }
    bool TouchingAny()
    {
        if (isTouchingDown)
            return true;
        if (isTouchingUp)
            return true;
        if (isTouchingLeft)
            return true;
        if (isTouchingRight)
            return true;
        return false;
    }
    void SlowBounce()
    {
        if(isTouchingUp && goingUp)
        {
            ChangeUpDir();
        }
        else if(isTouchingDown && !goingUp)
        {
            ChangeUpDir();
        }
        if (isTouchingRight)
        {
            FlipDir();
        }
        else if (isTouchingLeft)
        {
            FlipDir();
        }
        rb.velocity = slowSpeed * slowBounceDir;
    }
    void FastBounce()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeUpDir();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeUpDir();
        }
        if (isTouchingRight)
        {
            FlipDir();
        }
        else if (isTouchingLeft)
        {
            FlipDir();
        }
        rb.velocity = fastSpeed * fireDir.normalized;
    }
    private void FastFire()
    {
        fireDir = player.position - transform.position;
        StartCoroutine(WaitBounce());
    }
    IEnumerator WaitBounce()
    {
        yield return new WaitForSeconds(.2f);
        debugFastBounce = true;
    }
    IEnumerator AimingTimer()
    {
        fireDir = Vector3.zero;
        rb.velocity = Vector2.zero;
        debugFastBounce = false;
        state = BrotherSunState.FastBounce;
        Debug.Log("Aiming");
        yield return new WaitForSeconds(.5f);
        Debug.Log("FIRE");
        FastFire();
        aimingRoutine = null;
    }
    void ChangeUpDir()
    {
        goingUp = !goingUp;
        float amount = Random.Range(.1f, 1f);
        if (!goingUp)
            amount = -amount;
        slowBounceDir.y = amount;
        slowBounceDir.Normalize();

        fireDir.y *= -1;
    }
    void FlipDir()
    {
        goingRight = !goingRight;
        float amount = Random.Range(.1f, 1f);
        if (!goingRight)
            amount = -amount;
        slowBounceDir.x = amount;
        slowBounceDir.Normalize();

        fireDir.x *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(topSensor.position, sensorRadius);
        Gizmos.DrawWireSphere(bottomSensor.position, sensorRadius);
        Gizmos.DrawWireSphere(rightSensor.position, sensorRadius);
        Gizmos.DrawWireSphere(leftSensor.position, sensorRadius);
    }
}
