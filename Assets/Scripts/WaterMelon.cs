using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMelon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range;
    [SerializeField] float fireCd;
    private float fireTimer;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(Vector2.Distance(player.position, transform.position) <= range)
        {
            if(fireTimer <= 0)
            {
                fireTimer = fireCd;
                Shoot();
            }
        }
        fireTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        Debug.Log("Water Blast!");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<WaterBullet>().InitBullet(player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
