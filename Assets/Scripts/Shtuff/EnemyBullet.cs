using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float lifeTime;

    private int damage;
    // Start is called before the first frame update
    
    public void InitBullet(Vector3 playerPos, int gunDamage, float gunSpeed)
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = playerPos - transform.position;
        damage = gunDamage;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * gunSpeed;
        Vector2 rot = transform.position - playerPos;
        if (rot.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, 1);
        }
        float rotation = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
