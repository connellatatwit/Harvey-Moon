using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrotherSunState
{
    Sleeping,
    SlowBounce,
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

    [Header("Basic Stats")]
    [SerializeField] int maxHealth;
    private int currentHealth;
    private bool halfHealth = false;

    [Header("SlowBounce")]
    [SerializeField] float slowSpeed;

    [Header("FastBounce")]
    [SerializeField] float fastSpeed;

    [Header("LockedState")]
    [SerializeField] int numbSweeps;

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

    }
        else if (state == BrotherSunState.FastBounce)
        {
            /*
             * Scan for walls and pause when hit wall.
             * Aim body at player when hit wall and then fire self at players drection
             */
        }
        else if (state == BrotherSunState.Locked)
        {
            /*
             * Fire a laser that sweeps left to right and then another from right to left
             */
        }
    }
}
