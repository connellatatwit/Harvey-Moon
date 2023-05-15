using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dandelion : MonoBehaviour, IWeed, Enemy
{
    [SerializeField] GameObject dandelionBulletPrefab;

    [SerializeField] float health;
    [SerializeField] Transform shootPos;
    [SerializeField] float shootCd;
    private float shootTimer;

    [SerializeField] int value;

    [Header("Flash Stuff")]
    [SerializeField] Material flashMat;
    private Material originalMat;
    private Coroutine flashRoutine;
    [SerializeField] float duration = .125f;
    [SerializeField] SpriteRenderer sr;

    public int Value => value;

    private void Start()
    {
        shootTimer = Random.Range(shootCd - .1f, shootCd + .1f);
        originalMat = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            shootTimer = shootCd;
            Shoot();
        }
    }

    private void Shoot()
    {
        int amountShot = Random.Range(3, 6);
        for (int i = 0; i < amountShot; i++)
        {
            Vector2 dir = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            GameObject bullet = Instantiate(dandelionBulletPrefab, shootPos.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * 5;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            Destroy(gameObject);
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    public IEnumerator FlashRoutine()
    {
        sr.material = flashMat;
        yield return new WaitForSeconds(duration);
        sr.material = originalMat;
        flashRoutine = null;
    }
}
