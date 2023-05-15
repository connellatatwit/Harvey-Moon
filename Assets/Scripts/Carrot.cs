using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour, Enemy
{
    [SerializeField] GameObject droppedCarrotPrefab;
    [SerializeField] int maxHealth;
    private int currentHealth;
    [SerializeField] List<GameObject> possibleWeeds;

    [Header("Flash Stuff")]
    [SerializeField] Material flashMat;
    private Material originalMat;
    private Coroutine flashRoutine;
    [SerializeField] float duration = .125f;
    [SerializeField] SpriteRenderer sr;

    public GameObject Drop
    {
        get { return droppedCarrotPrefab; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        originalMat = sr.material;
    }
    public List<GameObject> PossibleWeeds
    {
        get { return possibleWeeds; }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Instantiate(droppedCarrotPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(flashRoutine != null)
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
