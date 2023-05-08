using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandelionBullet : MonoBehaviour
{
    [SerializeField] float lifeTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().Stun(.75f);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerMovement>().Stun(.75f);
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
