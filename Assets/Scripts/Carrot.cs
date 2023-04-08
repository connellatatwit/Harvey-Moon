using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour, Enemy
{
    [SerializeField] GameObject droppedCarrotPrefab;
    [SerializeField] int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Instantiate(droppedCarrotPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
