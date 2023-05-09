using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : MonoBehaviour, IBullet
{
    private float speed;
    private int damage;

    private bool player;

    [SerializeField] float lifeTime;
    [SerializeField] int amountOfHits;
    [SerializeField] GameObject bulletPrefab;
    private Rigidbody2D rb;

    public void InitBullet(Vector3 mousePos, int gunDamage, float gunSpeed, bool player, int amountOfHits)
    {
        rb = GetComponent<Rigidbody2D>();
        this.player = player;
        damage = gunDamage;
        speed = gunSpeed;
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        Vector2 rot = transform.position - mousePos;
        if (rot.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, 1);
        }
        float rotation = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotation);

        this.amountOfHits = amountOfHits;
    }
}
