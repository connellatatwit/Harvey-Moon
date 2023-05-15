using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabGrass : MonoBehaviour, IWeed, Enemy
{
    [SerializeField] int health;
    [SerializeField] GameObject slashPrefab;
    [SerializeField] float slashCd;
    private float slashTimer;
    [SerializeField] float range;
    [SerializeField] int weedValue;
    private Transform player;
    private Animator anim;

    [Header("Flash Stuff")]
    [SerializeField] Material flashMat;
    private Material originalMat;
    private Coroutine flashRoutine;
    [SerializeField] float duration = .125f;
    [SerializeField] SpriteRenderer sr;
    public int Value => weedValue;

    private void Start()
    {
        anim = GetComponent<Animator>();
        slashTimer = 1f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalMat = sr.material;
    }
    private void Update()
    {
        slashTimer -= Time.deltaTime;
        if(slashTimer <= 0)
        {
            if(Vector2.Distance(player.position, transform.position) <= range)
            {
                anim.SetTrigger("Slash");
                slashTimer = slashCd;
            }
        }
    }

    public void Slash()
    {
        bool flip = false;
        if (transform.position.x < player.position.x)
            flip = true;
        GameObject slashObj = Instantiate(slashPrefab, player.transform.position, Quaternion.identity);
        if (flip)
        {
            slashObj.transform.localScale = new Vector3(-slashObj.transform.localScale.x, slashObj.transform.localScale.y, 1);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
